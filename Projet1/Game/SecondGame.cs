using System;
using System.Threading;

namespace Projet1
{
    public class SecondGame
    {
        private static Game _game;
        private static int _ptsVictory;
        private static int _ptsDefeat;
        private static Random rdm = new Random();

        private static string idCurrentCard = "";
        
        public static void Start(Game g, int width, int height, int nbrCards, int nbrPtsVictory, int nbrPtsDefeat)
        {
            _game = g;
            _game.IdGame = 1;
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
            
            Thread.Sleep(5000);
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
                        ReturnHazardCard();
                        _game.NextPlayer();
                    }
                }
                
            }
        }

        private static void ReturnHazardCard()
        {
            Card c;
            do
            {
                c = Program.cm.GetCards()[rdm.Next(0, Program.cm.GetCards().Count)];
            } while (c.face);
            
            c.face = true;
            idCurrentCard = c.id;
            
        }
    }
}