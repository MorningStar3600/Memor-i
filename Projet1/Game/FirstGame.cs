using System;
using System.Collections.Generic;

namespace Projet1
{
    static class FirstGame
    {
        private static Game _game;
        public static void Start(Game g, int width, int height)
        {
            _game = g;
            string[] cardName = new string[] {"Menu/moins", "Menu/plus", "Menu/plus", "Menu/moins","2","2"};
            string[] cardBackName = new string[] {"1","1","1","1","1","1"};
            Program.LoadCardManager(cardName, cardBackName, 3, EventHandler, width-g.GetNumbPlayers()*4-1, height, 0.5, g);
            ScoreTracker sc = new ScoreTracker(g, width-g.GetNumbPlayers()*4, 0, 30);
            Draws.toDraw.Add(sc);
        }

        private static void EventHandler(CardManager cm, int cardId, int eventId)
        {
            if (eventId == 1)
            {
                if (cm.GetCards()[cardId].face == false)
                {
                    cm.GetCards()[cardId].selected = true;
                    cm.GetCards()[cardId].face = true;
                }
                else if (cm.GetCards()[cardId].selected)
                {
                    cm.GetCards()[cardId].selected = false;
                    cm.GetCards()[cardId].face = false;
                }
                
                List<Card> visibleCards = new List<Card>();
                for (int i = 0; i < cm.GetCards().Count; i++)
                {
                    if (cm.GetCards()[i].selected)
                    {
                        visibleCards.Add(cm.GetCards()[i]);
                    }
                }

                if (visibleCards.Count == 2)
                {

                    if (visibleCards[0].id == visibleCards[1].id)
                    {
                        _game.AddScore(_game.GetCurrentPlayer(), 100);
                        _game.NextPlayer();
                        visibleCards[0].selected = false;
                        visibleCards[1].selected = false;
                        visibleCards[0].face = true;
                        visibleCards[1].face = true;
                    }
                    else
                    {
                        _game.AddScore(_game.GetCurrentPlayer(), -50);
                        _game.NextPlayer();
                        visibleCards[0].selected = false;
                        visibleCards[1].selected = false;
                        visibleCards[0].face = false;
                        visibleCards[1].face = false;
                    }
                }
            }
        }
    }
}