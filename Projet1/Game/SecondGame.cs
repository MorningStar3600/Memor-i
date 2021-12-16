using System;
using System.Threading;
using Projet1.Menu;

namespace Projet1
{
    public class SecondGame
    {
        private static Game _game;
        private static int _ptsVictory;
        private static int _ptsDefeat;
        private static Random rdm = new Random();
        private static int _width;
        private static int _height;

        private static string idCurrentCard = "";
        
        public static void Start(Game g, int width, int height, int nbrCards, int nbrPtsVictory, int nbrPtsDefeat)
        {
            _game = g;
            _game.IdGame = 1;
            _width = width;
            _height = height;
            
            string[] cardName = RandomizedCard.GetCardPair(nbrCards/2);
            string[] cardBackName = new string[nbrCards];
            for (int i =0; i < nbrCards; i++)
            {
                cardBackName[i] = "1";
            }
            
            Program.LoadCardManager(cardName, cardBackName, 5, EventHandler, width-g.GetNumbPlayers()*4-1, height, Config.AnimationSpeed, true, g);
            ScoreTracker sc = new ScoreTracker(g, width-g.GetNumbPlayers()*4, 0, 30);
            Draws.toDraw.Add(sc);
            
            _ptsVictory = nbrPtsVictory;
            _ptsDefeat = nbrPtsDefeat;

            for (int i = 4; i >= 0; i--)
            {
                Draws.toDraw.Add(new UpdatedText((i+1).ToString(), 0, 0));
                Thread.Sleep(1000);
            }
            Draws.toDraw.Add(new UpdatedText(" ", 0, 0));
            
            for (int i = 0; i < Program.cm.GetCards().Count; i++)
            {
                Program.cm.GetCards()[i].face = false;
            }
            
            ReturnHazardCard();
            
        }

        private static void EventHandler(CardManager cm, int cardId, int eventId, char key, int keyCode)
        {
            if (eventId == 1)
            {
                if (cm.GetCards()[cardId].face == false)
                {
                    cm.GetCards()[cardId].face = true;
                    if (cm.GetCards()[cardId].id != idCurrentCard)
                    {
                        _game.GetCurrentPlayer().AddScore(_ptsDefeat);
                        Thread.Sleep(1000);
                        cm.GetCards()[cardId].face = false;
                    }
                    else
                    {
                        _game.GetCurrentPlayer().AddScore(_ptsVictory);
                        Thread.Sleep(100);
                        ReturnHazardCard();
                        _game.NextPlayer();
                    }
                }
                
            }
        }

        private static void ReturnHazardCard()
        {
            Card c;
            int count = 0;
            for (int i = 0; i < Program.cm.GetCards().Count; i++)
            {
                if (Program.cm.GetCards()[i].face == false)
                {
                    count++;
                }
            }

            if (count > 0)
            {
                do
                {
                    c = Program.cm.GetCards()[rdm.Next(0, Program.cm.GetCards().Count)];
                } while (c.face);
            
                c.face = true;
                idCurrentCard = c.id;
            }
            else
            {
                WinMenu.Start(_width, _height, _game);
            }
            
            
        }
    }
}