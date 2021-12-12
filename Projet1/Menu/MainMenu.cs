namespace Projet1.Menu
{
    class MainMenu
    {
        int _width;
        int _height;
        public MainMenu(int width, int height)
        {
            _width = width;
            _height = height;
        }

        public void Load()
        {
            string[] name = {"1","1"};
            //{"1","1","1","1","1","1","1","1","1","1","1","1","1","1","1","1","1","1","1","1"};
            string[] backName = {"1","1"};
            //{"1","1","1","1","1","1","1","1","1","1","1","1","1","1","1","1","1","1","1","1"};
            Program.LoadCardManager(name, backName, 2, Hover, _width,_height, 0.58);
        }

        void Hover(CardManager cm, int cardId, int eventId)
        {
            if (eventId == 1)
            {
                switch (cardId)
                {
                    case 0 :
                    {
                        string[] name = {"Menu/plus","1","1","1","1","1","1","1","1","1","1","1","1","1","1","1","1","1","1","1"};
                        //{"1","1","1","1","1","1","1","1","1","1","1","1","1","1","1","1","1","1","1","1"};
                        string[]  backName ={"Menu/plus","Menu/moins","Menu/moins","Menu/moins","Menu/moins","Dos","Menu/moins","Menu/moins","Menu/moins","Menu/moins","Menu/moins","Menu/moins","1","1","1","1","1","1","1","1"};
                        //{"1","1","1","1","1","1","1","1","1","1","1","1","1","1","1","1","1","1","1","1"};
                        Program.LoadCardManager(name, backName, 5, GameHover, _width,_height, 0.5);
                        break;
                    }
                    case 1:
                    {
                        Game g = new Game(0, new string[] {"Eleonore","Alexandre"});
                        FirstGame.Start(g, _width, _height);
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