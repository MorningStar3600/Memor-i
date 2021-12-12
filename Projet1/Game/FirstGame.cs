using System;
using System.Collections.Generic;

namespace Projet1
{
    static class FirstGame
    {
        private static Game _game;
        private static int _ptsVictory;
        private static int _ptsDefeat;
        public static void Start(Game g, int width, int height, int nbrCards, int nbrPtsVictory, int nbrPtsDefeat)
        {
            _game = g;
            string[] cardName = RandomizedCard.GetCardPair(nbrCards/2);
            string[] cardBackName = new string[nbrCards];
            for (int i =0; i < nbrCards; i++)
            {
                cardBackName[i] = "1";
            }
            
            Program.LoadCardManager(cardName, cardBackName, 5, EventHandler, width-g.GetNumbPlayers()*4-1, height, 0.5, false, g);
            ScoreTracker sc = new ScoreTracker(g, width-g.GetNumbPlayers()*4, 0, 30);
            Draws.toDraw.Add(sc);
            
            _ptsVictory = nbrPtsVictory;
            _ptsDefeat = nbrPtsDefeat;
        }

        private static void EventHandler(CardManager cm, int cardId, int eventId, char key, int keyCode)
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
                        _game.AddScore(_game.GetCurrentPlayer(), _ptsVictory);
                        _game.NextPlayer();
                        visibleCards[0].selected = false;
                        visibleCards[1].selected = false;
                        visibleCards[0].face = true;
                        visibleCards[1].face = true;
                    }
                    else
                    {
                        _game.AddScore(_game.GetCurrentPlayer(), _ptsDefeat);
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