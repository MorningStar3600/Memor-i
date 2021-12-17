using System;
using System.Collections.Generic;
using System.Threading;
using Projet1.Menu;

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
            _game.IdGame = 0;
            string[] cardName = RandomizedCard.GetCardPair(nbrCards/2);
            string[] cardBackName = new string[nbrCards];
            for (int i =0; i < nbrCards; i++)
            {
                cardBackName[i] = "1";
            }
            
            Program.LoadCardManager(cardName, cardBackName, 5, EventHandler, width-g.GetNumbPlayers()*4-1, height, Config.AnimationSpeed, false, g);
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
                
                List<Card> selectedCards = new List<Card>();
                
                for (int i = 0; i < cm.GetCards().Count; i++)
                {
                    if (cm.GetCards()[i].selected)
                    {
                        selectedCards.Add(cm.GetCards()[i]);
                    }
                }
                if (selectedCards.Count == 2)
                {

                    if (selectedCards[0].id == selectedCards[1].id)
                    {
                        _game.AddScore(_game.GetCurrentPlayer(), _ptsVictory);
                        _game.NextPlayer();
                        selectedCards[0].selected = false;
                        selectedCards[1].selected = false;
                        selectedCards[0].face = true;
                        selectedCards[1].face = true;
                    }
                    else
                    {
                        _game.AddScore(_game.GetCurrentPlayer(), _ptsDefeat);
                        _game.NextPlayer();
                        selectedCards[0].selected = false;
                        selectedCards[1].selected = false;
                        Thread.Sleep(1000);
                        selectedCards[0].face = false;
                        selectedCards[1].face = false;
                    }
                }
                List<Card> hiddenCards = new List<Card>();
                for (int i = 0; i < cm.GetCards().Count; i++)
                {
                    if (cm.GetCards()[i].face == false)
                    {
                        hiddenCards.Add(cm.GetCards()[i]);
                    }
                }
                
                if (hiddenCards.Count == 0)
                {
                    Thread.Sleep(2000);
                    int maxPlayer = 0;
                    for (int k = 0; k < _game.GetNumbPlayers(); k++)
                    {
                        /*if (_game._players[k].GetScore() < _game._players[maxPlayer].GetScore())
                        {
                            maxPlayer = k;
                        }*/
                    }
                    
                    for (int k = 0; k < _game.GetNumbPlayers(); k++)
                    {
                        if (k != maxPlayer)
                        {
                            _game.Lose(_game._players[k]);
                        }
                    }
                    _game.Win(_game._players[maxPlayer]);
                    
                    WinMenu.Start(CardManager.WindowsWidth, CardManager.WindowsHeight, _game);
                }
            }
        }
    }
}