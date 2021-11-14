using System;
using System.Collections.Generic;
using System.Threading;

namespace Projet1
{
    class Map
    {
        public List<Tuple<Card, DrawItem>> cards { get; set; } = new List<Tuple<Card, DrawItem>>();

        public Map(Card[] cards)
        {
            for (int i = 0; i < cards.Length; i++)
            {
                Tuple<Card, DrawItem> c = new Tuple<Card, DrawItem>(cards[i], null);
                this.cards.Add(c);
            }
        }

        public void Draw()
        {
            Random rdm = new Random();
            DrawItem[] dd = new DrawItem[cards.Count];
            for (int i = 0; i < cards.Count; i++)
            {
                dd[i] = cards[i].Item1.switchVisibility(10);
               
            }
            Console.ReadKey();
            for (int i = 0; i < cards.Count; i++)
            {
                dd[i] = cards[i].Item1.switchCard(rdm.Next(8, 16), dd[i]);
            }
            Console.ReadKey();
            for (int i = 0; i < cards.Count; i++)
            {
                dd[i] = cards[i].Item1.switchCard(rdm.Next(8, 16), dd[i]);
            }
            Console.ReadKey();
            for (int i = 0; i < cards.Count; i++)
            {
                dd[i] = cards[i].Item1.switchCard(rdm.Next(8, 16), dd[i]);
            }
            Console.ReadKey();
            for (int i = 0; i < cards.Count; i++)
            {
                dd[i] = cards[i].Item1.switchCard(rdm.Next(8, 16), dd[i]);
            }
            Console.ReadKey();
            for (int i = 0; i < cards.Count; i++)
            {
                cards[i].Item1.switchVisibility(rdm.Next(8, 12), dd[i]);
            }
        }

        public static (int[,], int) getCardConfiguration(int nbr, int nbrCardInARow)
        {
            var nbrCardsInRow = nbrCardInARow;
            var nbrColumn = (int) Math.Ceiling(nbr/(double)nbrCardsInRow);

            int[,] pos = new int[nbr,2];
            int maxWidth =(Console.WindowWidth/nbrCardsInRow) - nbrCardsInRow-1;
            int maxHeight = (Console.WindowHeight/nbrColumn) - nbrColumn-1;
            for (int i = 0; i < nbr; i++)
            {
                int index = (i - nbrCardsInRow * (i / nbrCardsInRow));
                pos[i, 0] = (index+1) * (Console.WindowWidth / (nbrCardsInRow)) - maxWidth/2; 
                //pos[i, 0] = (i-nbrCardsInRow*(i/nbrCardsInRow))*(maxWidth)+1;
                pos[i, 1] = (i/nbrCardsInRow)*(maxHeight)+(i/nbrCardsInRow);
            }
            
            

            return (pos, maxWidth);
            


        }
        
        
    }
}