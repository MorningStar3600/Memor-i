using System;
using System.Runtime.CompilerServices;

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
            string[] cards = {"Menu/moins","Stars","Menu/plus","default","default","Enter"};
            string[] back = {"default","default","default","default","default","default"};
            Program.LoadCardManager(cards, back, 3, EventHandler, width, height, 0, true);
            
            DifficultyMenu.game = game;
            DifficultyMenu.start = start;
            DifficultyMenu.width = width;
            DifficultyMenu.height = height;
        }

        private static void EventHandler(CardManager cm, int cardId, int eventId, char key, int keyCode)
        {
            if (eventId == 1)
            {
                if (cardId == 2)
                {
                    cm.GetCards()[1].NextAnimation();
                    SetDifficulty(cm);
                }

                if (cardId == 0)
                {
                    cm.GetCards()[1].PrevAnimation();
                    SetDifficulty(cm);
                }
            }else if (eventId == 2)
            {
                if (keyCode == 13)
                {
                    start(game, width, height, nbrCard, nbrPointsByWin, nbrPointsByLoose);
                }
            }
        }

        private static void SetDifficulty(CardManager cm)
        {
            switch(cm.GetCards()[1].animationIndex)
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
    }
}