using System;
using System.Collections.Generic;
using System.Threading;

namespace Threads.ThreadDispatcher
{
    public class ThreadDispatcher
    {
        private static ThreadDispatcher instance;
        private Thread cleaner = new Thread(Clean);
        private static List<ThreadWorker> _workers = new();


        private ThreadDispatcher()
        {
            for (var i = 0; i < Environment.ProcessorCount; i++) _workers.Add(new ThreadWorker());
            cleaner.Start();
        }

        public static ThreadDispatcher GetInstance() => instance ??= new ThreadDispatcher();

        private static void Clean()
        {
            while (true)
            {
                _workers.RemoveAll(x => !(x.FromThreadPool || x.IsRunning));
                Thread.Sleep(50);
            }
        }

        public void Run()
        {
            for (var i = 0; i < _workers.Count; i++) _workers[i].Run();
        }


        TaskQueue _queue = TaskQueue.GetInstance();

        public void Add(IThreadedTask task)
        {
            var worker = new ThreadWorker(task);
            worker.Run();
            lock (_workers)
            {
                _workers.Add(worker);
            }
        }

        public void AddInQueue(IThreadedTask task)
        {
            this._queue.AddTask(task);
        }
    }
}