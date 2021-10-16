using System.Collections.Generic;

namespace Projet1
{
    public class Map
    {
        private List<Card> cards = new List<Card>();

        public Map()
        {
            cards.Add(new Card(1, 10, "1"));
        }

        public void Draw()
        {
            for (int i = 0; i < cards.Count; i++)
            {
                cards[i].Draw();
            }
        }
    }
}