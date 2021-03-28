using System.Collections.Generic;

namespace Threads.ThreadDispatcher
{
    internal class ThreadMonitor : TaskQueue, IThreadedTask
    {
        private List<IThreadedTask> _tasks;

        internal ThreadMonitor(List<IThreadedTask> t)
        {
            _tasks = t;
        }

        public void Run()
        {
            //todo
        }
    }
}