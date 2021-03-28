using Threads.ThreadDispatcher;

namespace Threads
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadDispatcher.ThreadDispatcher.GetInstance().Run();
            ThreadDispatcher.ThreadDispatcher.GetInstance().AddInQueue(new FirstTask());
        }
    }
}