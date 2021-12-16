using System;
using System.Threading;

namespace Projet1.Menu
{
    public static class PlayerSelectionMenu
    {
        private static Game _g;
        private static int _width;
        private static int _height;
        private static int time = 0;
        private static int _nbPlayer = 1;
        public static void Start(int width, int height, Game g)
        {
            _g = g;
            _width = width;
            _height = height;
            
            string[] cards = new string[18];
            for (int i = 0; i < 18; i++)
            {
                if (i < g.GetNumbPlayers())
                {
                    cards[i] = "Menu/player";
                }else if (i == g.GetNumbPlayers())
                {
                    cards[i] = "Menu/moins";
                }else if (i == g.GetNumbPlayers() + 1)
                {
                    cards[i] = "Menu/plus";
                }
                else
                {
                    cards[i] = "default";
                }
            }
            
            string[] back = {"default","default","default","default","default","default","default","default","default","default","default","default","default","default","default","default","default","default"};
            Draws.toDraw.Clear();
            Draws.Clear();
            Program.cm = null;
            Program.cm = new CardManager(cards, back, 6, EventHandler, width, height, 0, true)
            {
                hoverHandle = false
            };
            Program.cm.Draw();
            for (int i =0; i < _g.GetNumbPlayers(); i++)
            {
                Program.cm.GetCards()[i].Select(true, _g._players[i].character, _g._players[i].color);
            }
        }

        private static void EventHandler(CardManager cm, int cardId, int eventId, char key, int keyCode)
        {
            for (int i =0; i < _g.GetNumbPlayers(); i++)
            {
                Program.cm.GetCards()[i].Select(true, _g._players[i].character, _g._players[i].color);
            }

            if (eventId == 1)
            {
                if (cardId == _g.GetNumbPlayers()+1)
                {
                    _g.AddPlayer("test2", ConsoleColor.White, 'H');
                    Start(_width, _height, _g);
                }
                else if (cardId == _g.GetNumbPlayers())
                {
                    _g.RemovePlayer();
                    Start(_width, _height, _g);
                }
            }
        }
    }
}