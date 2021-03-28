using System;
using Threads.ThreadDispatcher;

namespace Threads
{
    public class SecondTask:IThreadedTask
    {
        public void Run()
        {
            Console.WriteLine("second");
        }
    }
}