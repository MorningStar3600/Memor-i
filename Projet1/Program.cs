using System;
using System.Data.OleDb;
using System.Security.Cryptography.X509Certificates;

namespace Projet1
{
    internal class Program
    {
        public static int x = 0;
        public static int y = 0;
        public static void Main(string[] args)
        {
            
            int oldX = -1;
            int oldY = -1;
            
            //ConsoleListener.run();
            
            Map m = new Map();
            m.Draw();
            Console.ReadKey();

            //ConsoleListener.run();
            while (false)
            {
                if (x != oldX || y != oldY)
                {
                    Console.SetCursorPosition(0,0);
                    Console.WriteLine(x + " ");
                    Console.WriteLine(y + " ");
                    oldX = x;
                    oldY = y;
                }
                
            }
        }
    }
}