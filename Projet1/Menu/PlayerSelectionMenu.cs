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
                cards[i] = "Menu/player";
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
            
            Program.cm.GetCards()[0].animationIndex = 3;
            Program.cm.GetCards()[1].animationIndex = 2;
            Program.cm.GetCards()[2].animationIndex = 1;
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
                        Program.cm.GetCards()[cardId].NextAnimation();
                        Program.cm.GetCards()[cardId-1].NextAnimation();
                    }
                }
            
                if (cardId == _g.GetNumbPlayers())
                {
                    Draws.toDraw.Add(new UpdatedText("What??", 50, 0));
                    if (_g.GetNumbPlayers() >= 2)
                    {
                        _g.RemovePlayer();
                        Start(_width, _height, _g);
                    }
                }
            
                if (cardId < _g.GetNumbPlayers())
                {
                    Draws.toDraw.Add(new UpdatedText("What???", 50, 0));
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