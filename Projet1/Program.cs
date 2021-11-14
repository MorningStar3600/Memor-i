using System;
using System.Data.OleDb;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace Projet1
{
    internal class Program
    {
        public static int x = 0;
        public static int y = 0;
        public static CardManager cm;
        public static void Main(string[] args)
        {
            ConsoleManager.SetCurrentFont("Consolas", 20);
            Config.initiate();
            
            //Draws.Run();
            
            int oldX = -1;
            int oldY = -1;

            FastConsole.Write(new FastConsole.CharInfo[10000], 0, 0, 100, 100);
            //ConsoleListener.run();
            
            string[] name = {"1","2","1"};
            cm = new CardManager(name, 3, Hover);
            
            cm.Draw();
            Thread.Sleep(1000);
            ConsoleManager.SetCurrentFont("Consolas", 20);
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

        public static void SetColor(int x, int y)
        {
            if (x < 100 && y < 100)
            {
                //if (m.cards[0].Item1.color == ConsoleColor.White) m.cards[0].Item1.color = ConsoleColor.Red;
                //else m.cards[0].Item1.color = ConsoleColor.White;
            }
        }

        public static void Hover(CardManager cm, int cardId, int eventId)
        {
            if (eventId == 1)
            {
                cm.GetCards()[cardId].switchCard(1);
                
            }
        }

    }
}