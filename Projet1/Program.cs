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

            Config.initiate();
            Draws.Run();
            
            int oldX = -1;
            int oldY = -1;

            //ConsoleListener.run();
            Card c = new Card(0, "1");
            Card cs = new Card(1, "1");
            Card ccs = new Card(2, "1");

            Map m = new Map(new Card[] {c, cs, ccs});
            m.Draw();
            Console.ReadKey();
            Console.ReadKey();
            Console.ReadKey();
            Console.ReadKey();
            Console.ReadKey();
            Console.ReadKey();
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