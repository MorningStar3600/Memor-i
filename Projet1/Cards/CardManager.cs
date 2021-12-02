using System;
using System.Collections.Generic;
using System.IO;
using System.Security.AccessControl;
using System.Threading;

namespace Projet1
{
    class CardManager
    {
        private List<Card> _cards = new List<Card>();
        private int _actualCard = -1;
        private Action<CardManager, int, int> _eventManager;

        public CardManager(string[] cards, int nbrCardsInRow, Action<CardManager, int, int> eventManager, bool normalize = true)
        {
            List<List<ColoredChar[]>> cardValues = new List<List<ColoredChar[]>>();

            int[] cardsMaxLineLength = new int[cards.Length];
            
            int maxCardWidth = (Console.WindowWidth / nbrCardsInRow) - 3;
            int nbrCardsInColumn = (int) Math.Ceiling((double) cards.Length / (double) nbrCardsInRow);
            int maxCardHeight = (Console.WindowHeight / nbrCardsInColumn)-1;
            _eventManager = eventManager;
            
            ProgressBar progressBar = new ProgressBar(Console.WindowWidth/2, Console.WindowHeight/2, cards.Length);
            
            for (int i = 0; i < cards.Length; i++)
            {
                int cWidth = CardLoader.Load(cards[i], cardValues);
                int x = 0;
                int y = 0;
                int width = 0;
                int height = 0;
                ComputeCardTransform(nbrCardsInRow, false, cWidth, cardValues[0].Count, i, maxCardHeight, maxCardWidth, out x, out y, out width, out height);
                this._cards.Add(new Card(i,cardValues, x, y, width, height));
                progressBar.Update();
            }

            for (int i = 0; i < cards.Length; i++)
            {
                Draws.toDraw.Add(this._cards[i]);
                Thread.Sleep(20);
            }
        }

        private void ComputeCardTransform(int nbrCardInRow, bool normalize, int cardWidth, int cardHeight, int index, int maxCardHeight, int maxCardWidth, out int x, out int y, out int width, out int height)
        {
            height = cardHeight > maxCardHeight ? maxCardHeight : cardHeight;
            double ratio2 = height / (double) cardHeight;
            width = cardWidth > maxCardWidth
                ? cardWidth * ratio2 > maxCardWidth ? maxCardWidth : (int) (cardWidth * ratio2)
                : cardWidth;
            
            y = (index / nbrCardInRow + 1) * maxCardHeight - maxCardHeight / 2 - height / 2;
            
            int indexInRow = index % nbrCardInRow;
            x = (indexInRow + 1) * maxCardWidth - maxCardWidth / 2 - width / 2;

            
        }
        
        public void EventHandling(int x, int y, int evt)
        {
            int cardIndex = -1;
            for (int i = 0; i < this._cards.Count; i++)
            {
                if (_cards[i].IsInCard(x, y))
                {
                    cardIndex = i;
                    break;
                }
            }
            //evt = 0 : when card is hovered
            //evt = 1 : when card is clicked
            
            if (_actualCard != -1)
            {
                if (_actualCard != cardIndex)
                {
                    _cards[_actualCard].Select(false);
                    _actualCard = -1;
                }
            }
            if (cardIndex != -1)
            {
                
                if (_actualCard != cardIndex)
                {
                    _cards[cardIndex].Select(true);
                    _actualCard = cardIndex;
                }
                _eventManager(this, cardIndex, evt);
            }
            
        }

        public void Draw()
        {
            foreach (Card card in _cards)
            {
                card.switchVisibility(2);
            }
        }

        public List<Card> GetCards()
        {
            return _cards;
        }
    }
}