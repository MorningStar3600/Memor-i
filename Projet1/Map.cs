using System;
using System.Collections.Generic;
using System.Threading;

namespace Projet1
{
    public class Map
    {
        private List<Card> cards = new List<Card>();

        public Map()
        {
            cards.Add(new Card(1, "1"));
        }

        public void Draw()
        {
            //for (int i = 0; i < cards.Count; i++)
            //{
            //    //this.cards[i].Draw(100000, 0, 11);
            //    //this.cards[i].DrawRotate(this.cards[i].GetWidth()+2, 11, 4);
            //    //this.cards[i].DrawRotate(2*this.cards[i].GetWidth()+4, 11, 8);

            //    //Card.Draw(this.cards[i].Rotate(5), 0, 3);  
            //    Card.Draw(this.cards[i].value, 0, 3);
            //    Card.Draw(Card.Rotate(Card.Smooth(Card.Resize(this.cards[i].value, this.cards[i].width - 10, this.cards[i].height)), 1), 90, 2);
            //    Card.Draw(Card.Rotate(Card.Smooth(Card.Resize(this.cards[i].value, this.cards[i].width - 20, this.cards[i].height)), 2), 170, 1);
            //    Card.Draw(Card.Rotate(Card.Smooth(Card.Resize(this.cards[i].value, this.cards[i].width - 30, this.cards[i].height)), 3), 240, 0);
            //}
            int n = 8; 
            for (int i = 0; i < n; i++)
            {
                Card.Draw(Card.Rotate(Card.Smooth(Card.Resize(this.cards[0].value, this.cards[0].width - 8*i, this.cards[0].height)), i), 90+4*i, n);
                Card.Clear(Card.Rotate(Card.Smooth(Card.Resize(this.cards[0].value, this.cards[0].width - 8 * i, this.cards[0].height)), i), 90 + 4 * i, n);
                
                //Console.Clear();
            }
        }
    }
}