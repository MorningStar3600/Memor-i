using System;

namespace Projet1
{
    public class ScopedTimer
    {
        private readonly int timeStamp;
        public ScopedTimer()
        {
            timeStamp = Environment.TickCount;
        }
        
        ~ScopedTimer()
        {
            Console.WriteLine("{0} ms", Environment.TickCount - timeStamp);
        }
    }
}