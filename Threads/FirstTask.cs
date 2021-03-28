using System;
using System.Linq;
using Threads.ThreadDispatcher;
using static System.Linq.Enumerable;

namespace Threads
{
    public class FirstTask:IThreadedTask
    {
        public void Run()
        {
            Console.WriteLine(Repeat(1, 100000000).Aggregate(((i, i1) => (int) (Math.Pow(i, 0.5) + i1))));
        }
    }
}