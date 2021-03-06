using System;
using System.Collections.Generic;
using System.IO;
using System.Security.AccessControl;
using System.Threading;
using Projet1.Menu;

namespace Projet1
{
    public class CardManager
    {
        public static int WindowsWidth = 0;
        public static int WindowsHeight = 0;
        
        private List<Card> _cards = new List<Card>();
        private int _actualCard = -1;
        private Action<CardManager, int, int, char, int> _eventManager;
        private bool _isClicked = false;
        private Game _game;
        public bool hoverHandle = true;
        
        
        public CardManager(string[] cards, string[] backs, int nbrCardsInRow, Action<CardManager, int, int, char, int> eventManager, int windowWidth, int windowHeight, double animationSpeed, bool startingFace = false, Game game = null) 
        {
            
            
            int maxCardWidth = (windowWidth / nbrCardsInRow);
            int nbrCardsInColumn = (int) Math.Ceiling((double) cards.Length / (double) nbrCardsInRow);
            int maxCardHeight = ((windowHeight-1)/ nbrCardsInColumn);
            
            _game = game;
            
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
                this._cards.Add(new Card(cards[i],cardValues, cardBackValues, x, y, width, height, maxCardWidth, maxCardHeight, animationSpeed, startingFace));
                //progressBar.Update();
            }
            
            _eventManager = eventManager;

            
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
        
        public void EventHandling(int x, int y, int evt, char key = ' ', int keyCode = -1)
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
                    if (hoverHandle)_cards[_actualCard].Select(false);
                    _actualCard = -1;
                }
            }
            if (cardIndex != -1 && _cards[cardIndex].id != "default")
            {
                if (_actualCard != cardIndex)
                {
                    if (_game != null && hoverHandle)
                    {
                        _cards[cardIndex].Select(true, _game.GetCurrentPlayer().character, _game.GetCurrentPlayer().color);
                    }
                    else if (hoverHandle)
                    {
                        _cards[cardIndex].Select(true, 'X', ConsoleColor.Red);
                    }
                
                    _actualCard = cardIndex;
                }
            
                if (evt == 1 && !_isClicked)
                {
                    if (_eventManager != null)
                    {
                        _eventManager(this, _actualCard, 1, ' ', 0);
                        _isClicked = true;
                    }
                
                }
                else if (evt == 0 && _isClicked)
                {
                    if (_eventManager != null)
                    {
                        _eventManager(this, _actualCard, 0, ' ', 0);
                        _isClicked = false;
                    }
                
                }
                
            }

            if (evt == 2)
            {
                if (keyCode == 27)
                {
                    if (Program.isInMenu)
                    {
                        Draws.toDraw.Clear();
                        Draws.Clear();
                        Program.cm = Program.onHoldCm;
                        Program.isInMenu = false;
                        Program.cm.Draw();
                    }
                    else
                    {
                        Draws.toDraw.Clear();
                        Draws.Clear();
                        Program.onHoldCm = Program.cm;
                        Program.isInMenu = true;
                        EscMenu.Start(WindowsWidth, WindowsHeight);
                    }
                }
                else
                {
                    _eventManager(this,_actualCard,2,key, keyCode);
                }
                
            }
            
        }
        
        public void Draw()
        {
            foreach (var t in _cards)
            {
                Draws.toDraw.Add(t);
                Thread.Sleep(20);
            }
        }
        
        public void Hide()
        {
            foreach (var t in _cards)
            {
                Draws.toDraw.Remove(t);
            }
        }
        
        public List<Card> GetCards()
        {
            return _cards;
        }

        public void FadeCards(bool fade)
        {
            if (fade)
            {
                for (int i = 0; i < _cards.Count; i++)
                {
                    _cards[i].Fade(true);
                }
            }
            else
            {
                for (int i = 0; i < _cards.Count; i++)
                {
                    _cards[i].Fade(false);
                }
            }
        }
    }
}