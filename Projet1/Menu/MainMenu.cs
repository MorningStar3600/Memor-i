using System;

namespace Projet1.Menu
{
    static class MainMenu
    {
        static int _width;
        static int _height;
        private static Game _game;

        public static void Start(int width, int height)
        {
            _width = width;
            _height = height;
            string[] name = {"default","default","default", "default","default","default","Menu/1","default","Menu/2","default","default","default","default","default", "default"};
            //{"1","1","1","1","1","1","1","1","1","1","1","1","1","1","1","1","1","1","1","1"};
            string[] backName = {"default","default","default","default","default","default","default","default","default","default","default","default","default","default","default"};
            //{"1","1","1","1","1","1","1","1","1","1","1","1","1","1","1","1","1","1","1","1"};
            Program.LoadCardManager(name, backName, 5, Hover, _width,_height, 0.58, true);
            _game = new Game(0, new string[] {"Eleonore","Alexandre"});
        }

        static void Hover(CardManager cm, int cardId, int eventId, char key, int keyCode)
        {
            if (eventId == 1)
            {
                switch (cardId)
                {
                    case 6 :
                    {
                        DifficultyMenu.Start(_game, _width, _height, FirstGame.Start);
                        break;
                    }
                    case 8:
                    {
                        DifficultyMenu.Start(_game, _width, _height, SecondGame.Start);
                        break;
                    }
                }
            }
        }
        
        public static void GameHover(CardManager cm, int cardId, int eventId)
        {
            
            if (eventId == 1)
            {
                if (cm.GetCards()[cardId].face)
                {
                    cm.GetCards()[cardId].face = false;
                }
                else
                {
                    cm.GetCards()[cardId].face = true;
                }
                 
            }
        }
    }
}