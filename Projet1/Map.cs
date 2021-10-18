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
            DrawItem[] dd = new DrawItem[3];
            for (int i = 0; i < cards.Count; i++)
            {
                dd[i] = cards[i].Item1.Draw(100*i, 0, cards[i].Item2);
               
            }
            Console.ReadKey();
            for (int i = 0; i < cards.Count; i++)
            {
                cards[i].Item1.switchCard(100 * i, 0, (i + 1) * 3, dd[i]);
            }
            Console.ReadKey();
            for (int i = 0; i < cards.Count; i++)
            {
                cards[i].Item1.switchCard(100 * i, 0, (i + 1) * 3, dd[i]);
            }
            Console.ReadKey();
            for (int i = 0; i < cards.Count; i++)
            {
                cards[i].Item1.switchCard(100 * i, 0, (i + 1) * 3, dd[i]);
            }
            Console.ReadKey();
            for (int i = 0; i < cards.Count; i++)
            {
                cards[i].Item1.switchCard(100 * i, 0, (i+1) * 3, dd[i]);
            }
        }
    }
}