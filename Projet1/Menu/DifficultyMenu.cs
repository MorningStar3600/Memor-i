using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Projet1.Menu
{
    public static class DifficultyMenu
    {
        //difficulty : (nbr card, nbr points by win, nbr points by loose)
        private static Action<Game, int, int, int, int, int> start;
        private static int width;
        private static int height;
        private static Game game;
        
        private static int nbrCard = 10;
        private static int nbrPointsByWin = 100;
        private static int nbrPointsByLoose = -25;
        public static void Start(Game game, int width, int height, Action<Game, int, int, int, int, int> start)
        {
            string[] cards = {"default","Menu/moins","Menu/player","Menu/plus","default","default","Menu/moins","Stars","Menu/plus","default","default","default","default","default","Enter"};
            string[] back = {"default","default","default","default","default","default","default","default","default","default","default","default","default","default","default"};
            Program.LoadCardManager(cards, back, 5, EventHandler, width, height, 0, true);
            
            DifficultyMenu.game = game;
            DifficultyMenu.start = start;
            DifficultyMenu.width = width;
            DifficultyMenu.height = height;
            
            
        }

        private static void EventHandler(CardManager cm, int cardId, int eventId, char key, int keyCode)
        {
            if (eventId == 1)
            {
                if (cardId == 1)
                {
                    cm.GetCards()[2].PrevAnimation();
                    SetNumberPlayer(cm);
                }
                if (cardId == 2)
                {
                    cm.GetCards()[2].NextAnimation();
                    SetNumberPlayer(cm);
                }
                if (cardId == 3)
                {
                    cm.GetCards()[2].NextAnimation();
                    SetNumberPlayer(cm);
                }
                if (cardId == 6)
                {
                    cm.GetCards()[7].PrevAnimation();
                    SetDifficulty(cm);
                }
                if (cardId == 7)
                {
                    cm.GetCards()[7].NextAnimation();
                    SetDifficulty(cm);
                }
                if (cardId == 8)
                {
                    cm.GetCards()[7].NextAnimation();
                    SetDifficulty(cm);
                }
                if (cardId == 14)
                {
                    start(game, width, height, nbrCard, nbrPointsByWin, nbrPointsByLoose);
                }
            }else if (eventId == 2)
            {
                if (keyCode == 13)
                {
                    start(game, width, height, nbrCard, nbrPointsByWin, nbrPointsByLoose);
                }

                if (keyCode == 37)
                {
                    cm.GetCards()[7].PrevAnimation();
                    SetDifficulty(cm);
                }
                
                if (keyCode == 39)
                {
                    cm.GetCards()[7].NextAnimation();
                    SetDifficulty(cm);
                }
            }
        }

        private static void SetDifficulty(CardManager cm)
        {
            switch(cm.GetCards()[7].animationIndex)
            {
                case 0:
                {
                    nbrCard = 10;
                    nbrPointsByWin = 100;
                    nbrPointsByLoose = -25;
                    break;  
                }

                case 1:
                {
                    nbrCard = 14;
                    nbrPointsByWin = 150;
                    nbrPointsByLoose = -50;
                    break;
                }
                case 2:
                {
                    nbrCard = 20;
                    nbrPointsByWin = 200;
                    nbrPointsByLoose = -100;
                    break;
                }
            }
        }

        private static void SetNumberPlayer(CardManager cm)
        {
            switch(cm.GetCards()[2].animationIndex)
            {
                case 0:
                {
                    game._players = new List<Player>() {new("Joueur 1") {color = ConsoleColor.Cyan, character = '1'}};
                    break;  
                }

                case 1:
                {
                    game._players = new List<Player>() {new("Joueur 1") {color = ConsoleColor.Cyan, character = '1'}, new("Joueur 2") {color = ConsoleColor.Green, character = '2'}};
                    break;
                }
                case 2:
                {
                    game._players = new List<Player>() {new("Joueur 1") {color = ConsoleColor.Cyan, character = '1'}, new("Joueur 2") {color = ConsoleColor.Green, character = '2'}, new("Joueur 3") {color = ConsoleColor.Red, character = '3'}};
                    break;
                }
            }
        }
    }
}