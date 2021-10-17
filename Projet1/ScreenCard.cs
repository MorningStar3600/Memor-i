using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet1
{
    class ScreenCard
    {
        int x { get; }
        int y { get; }
        List<string> card { get; }
        int state { get; }

        public ScreenCard(int x, int y, List<string> card, int state = 1)
        {
            this.x = x;
            this.y = y;
            this.card = card;
            this.state = state;
        }

        public void Draw()
        {
            if (state == 0)
            {
                this.Clear();
            }else if (state == 2)
            {
                this.Hide();
            }
            else
            {
                for (int i = 0; i < card.Count; i++)
                {
                    Console.SetCursorPosition(x, y + i);
                    Console.WriteLine(card[i]);
                }
            } 
        }

        public void Clear()
        {
            int maxWidth = Card.GetWidth(card);
            for (int i = 0; i < card.Count; i++)
            {
                Console.SetCursorPosition(x, y + i);
                char[] c = new char[maxWidth];
                for (int j = 0; j < maxWidth; j++)
                {
                    c[j] = ' ';
                }
                Console.Write(c);

            }
        }

        public void Hide()
        {
            int maxWidth = Card.GetWidth(card);
            for (int i = 0; i < card.Count; i++)
            {
                Console.SetCursorPosition(x, y + i);
                char[] c = new char[maxWidth];
                for (int j = 0; j < maxWidth; j++)
                {
                    if ((j == 0 || j == maxWidth - 1) || (i == 0 || i == card.Count - 1)) c[j] = 'X';
                    else c[j] = ' ';
                }
                Console.Write(c);

            }
        }
    }
}
