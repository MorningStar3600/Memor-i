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
            ConsoleManager.SetCurrentFont("Consolas", 8);
            Config.initiate();
            Draws.Run();
            ConsoleListener.run();

            string[] name = { "1", "2", "1", "2", "1", "2", "1", "2", };
            cm = new CardManager(name, 4, Hover);
            
            cm.Draw();
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