using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using Projet1.Menu;

namespace Projet1
{
    internal class Program
    {
        public static int x = 0;
        public static int y = 0;
        public static CardManager cm;
        public static void Main(string[] args)
        {
            Start();
        }

        public static void Start()
        {
            ConsoleManager.SetFullScreen();
            ConsoleManager.SetCurrentFont("Consolas",20);
            
            
            var width = Console.WindowWidth;
            var height = Console.WindowHeight;
            
            ConsoleManager.SetCurrentFont("Consolas",5);
            var miniWidth = Console.WindowWidth;
            var miniHeight = Console.WindowHeight;
            
            ConsoleManager.SetCurrentFont("Consolas",50);
            
            Utilities.DrawCenteredText("Memor'i");
            ProgressBar p = new ProgressBar(Console.WindowWidth/2,Console.WindowHeight/2+2, 25);
            LoadingScreen.PreLoad(p, miniWidth, miniHeight);
            ConsoleListener.run();
            Draws.Run();
            
            MainMenu m = new MainMenu(width,height);
            m.Load();

            
        }

        public static void SetColor(int x, int y)
        {
            if (x < 100 && y < 100)
            {
                //if (m.cards[0].Item1.color == ConsoleColor.White) m.cards[0].Item1.color = ConsoleColor.Red;
                //else m.cards[0].Item1.color = ConsoleColor.White;
            }
        }

        

        public static void LoadCardManager(string[] name, string[] backName, int nbCard,Action<CardManager, int, int, char, int> eventManager, int width, int height, double animationSpeed, bool startingFace = false, Game g = null)
        {
            LoadingScreen.Start();
            cm = new CardManager(name, backName, nbCard, eventManager, width, height, animationSpeed, startingFace, g);
            LoadingScreen.Stop();
            cm.Draw();
        }

    }
}