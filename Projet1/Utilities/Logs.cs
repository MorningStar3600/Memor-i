using System.Collections.Generic;
using System.IO;

namespace Projet1
{
    static class Logs
    {
        public static void Log(Queue<string> message)
        {
            while (message.Count > 0)
            {
                string line = message.Dequeue();
                using (StreamWriter sw = new StreamWriter("log.txt", true))
                {
                    sw.WriteLine(line);
                }
            }
        }
    }
}