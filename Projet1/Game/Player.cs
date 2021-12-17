using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Projet1
{
    public class Player
    {
        private string name { get; set; }
        public char character { get; set; }
        public ConsoleColor color { get; set; }

        //Pseudo|id jeu1\nbr victoires\nbr défaites\meilleur score|id jeu2\nbr victoires\nbr défaites\meilleur score

        public Player()
        {
            name = "";
        }

        public Player(string n)
        {
            name = n;
        }

        public string GetName()
        {
            return name;
        }

        public void Win(int idGame)
        {
            
        }

        public void Lose(int idGame)
        {
            
        }

        
    }
}