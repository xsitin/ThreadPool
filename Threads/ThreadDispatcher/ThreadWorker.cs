using System.Threading;

namespace Threads.ThreadDispatcher
{
    public class ThreadWorker
    {
        public Thread _thread;
        public readonly bool FromThreadPool;
        private bool isRunning;
        private static TaskQueue tq = TaskQueue.GetInstance();

        public ThreadWorker()
        {
            FromThreadPool = true;
            _thread = new Thread(Start);
        }

        public ThreadWorker(IThreadedTask task)
        {
            FromThreadPool = false;
            _thread = new Thread(task.Run);
        }

        public bool IsRunning
        {
            get => _thread.ThreadState == ThreadState.Running && isRunning;
            set => isRunning = value;
        }

        public void Run()
        {
            IsRunning = true;
            _thread.Start();
        }

        public void Stop()
        {
            IsRunning = false;
        }


        private void Start()
        {
            while (IsRunning)
            {
                IThreadedTask task;
                lock (tq)
                {
                    if (tq.Any())
                        task = tq.GetTask();

                    else
                    {
                        IsRunning = false;
                        tq.ContainTasks.WaitOne();
                        IsRunning = true;
                        continue;
                    }
                }

                task.Run();
            }
        }
    }
}