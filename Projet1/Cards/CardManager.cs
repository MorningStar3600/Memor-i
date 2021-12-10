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
        private bool isClicked = false;
        
        
        public CardManager(string[] cards, string[] backs, int nbrCardsInRow, Action<CardManager, int, int> eventManager, int windowWidth, int windowHeight, double animationSpeed, ConsoleColor selectColor = ConsoleColor.Blue)
        {
            
            
            int maxCardWidth = (windowWidth / nbrCardsInRow);
            int nbrCardsInColumn = (int) Math.Ceiling((double) cards.Length / (double) nbrCardsInRow);
            int maxCardHeight = ((windowHeight-1)/ nbrCardsInColumn);
            _eventManager = eventManager;
            
            //ProgressBar progressBar = new ProgressBar(windowWidth, windowHeight, cards.Length*2);
            
            for (int i = 0; i < cards.Length; i++)
            {
                List<List<ColoredChar[]>> cardValues = new List<List<ColoredChar[]>>();
                List<List<ColoredChar[]>> cardBackValues = new List<List<ColoredChar[]>>();
                int cWidth = CardLoader.Load(cards[i], cardValues);
                //progressBar.Update();
                CardLoader.Load(backs[i], cardBackValues);
                int x = 0;
                int y = 0;
                int width = 0;
                int height = 0;
                ComputeCardTransform(nbrCardsInRow, false, cWidth, cardValues[0].Count, i, maxCardHeight, maxCardWidth, out x, out y, out width, out height);
                this._cards.Add(new Card(int.Parse(cards[i]),cardValues, cardBackValues, x, y, width, height, maxCardWidth, maxCardHeight, animationSpeed, selectColor));
                //progressBar.Update();
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
                if (evt == 1 && !isClicked)
                {
                    _eventManager(this, _actualCard, 0);
                    isClicked = true;
                }
                else if (evt == 0 && isClicked)
                {
                    _eventManager(this, _actualCard, 1);
                    isClicked = false;
                }
            }
            
        }
        
        public void Draw()
        {
            for (int i = 0; i < this._cards.Count; i++)
            {
                Draws.toDraw.Add(this._cards[i]);
                Thread.Sleep(20);
            }
        }
        
        public List<Card> GetCards()
        {
            return _cards;
        }
    }
}