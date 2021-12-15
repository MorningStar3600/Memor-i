using System;

namespace Projet1.Menu
{
    public static class PlayerSelectionMenu
    {
        private static Game _g;
        private static int _width;
        private static int _height;
        public static void Start(int width, int height, Game g)
        {
            _g = g;
            _width = width;
            _height = height;
            
            string[] cards = new string[12];
            for (int i = 0; i < 12; i++)
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
            
            string[] back = {"default","default","default","default","default","default","default","default","default","default","default","default"};
            Draws.toDraw.Clear();
            Draws.Clear();
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
                    if (_g.GetNumbPlayers() <= 9)
                    {
                        _g.AddPlayer("init", ConsoleColor.White, 'i');
                        Start(_width, _height, _g);
                    }
                }else if (cardId == _g.GetNumbPlayers())
                {
                    if (_g.GetNumbPlayers() >= 2)
                    {
                        _g.RemovePlayer();
                        Start(_width, _height, _g);
                    }
                }
                else if (cardId < _g.GetNumbPlayers())
                {
                    _g._players[cardId].color++;
                    if ((int)_g._players[cardId].color == 16)
                    {
                        _g._players[cardId].color = 0;
                    }
                }
            }
        }
    }
}