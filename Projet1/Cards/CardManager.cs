using System;
using System.Collections.Generic;
using System.IO;

namespace Projet1
{
    class CardManager
    {
        private List<Card> _cards;
        private int _actualCard = -1;
        private Action<CardManager, int, int> _eventManager;

        public CardManager(string[] cards, int nbrCardsInRow, Action<CardManager, int, int> eventManager, bool normalize = true)
        {
            List<List<ColoredChar[]>> cardsValues = new List<List<ColoredChar[]>>();

            List<int> cardsMaxLineLength = new List<int>();
            LoadCards(cards, cardsMaxLineLength, cardsValues);
            this._cards = new List<Card>();
            this._eventManager = eventManager;

            int maxCardWidth = (Console.WindowWidth / nbrCardsInRow) - nbrCardsInRow - 1;
            int nbrCardsInColumn = (int) Math.Ceiling((double) cards.Length / (double) nbrCardsInRow);
            int maxCardHeight = (Console.WindowHeight / nbrCardsInColumn) - nbrCardsInColumn - 1;
            int minimalCardHeight = cardsValues[0].Count;
            for (int i = 1; i < cards.Length; i++)
            {
                if (cardsValues[i].Count < minimalCardHeight)
                {
                    minimalCardHeight = cardsValues[i].Count;
                }
            }
            
            int NormalizedCardHeight = Math.Min(maxCardHeight, minimalCardHeight);
            for (int i = 0; i < cards.Length; i++)
            {

                ComputeCardTransform(nbrCardsInRow, normalize, cardsMaxLineLength, i, maxCardHeight, maxCardWidth, cardsValues, NormalizedCardHeight, nbrCardsInColumn, out var width, out var height, out var x,
                    out var y);
                this._cards.Add(new Card(i, cardsValues[i], x, y, width, height));
                Console.WriteLine("x:" + x + " y:" + y + " width:" + width + " height:" + height);
            }
            for (int i = 0; i < this._cards.Count; i++)
            {
                Draws.toDraw.Add(this._cards[i]);
            }
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

            if (cardIndex != -1)
            {
                
                if (_actualCard != cardIndex)
                {
                    _cards[cardIndex].SetColor(ConsoleColor.Red);
                    _actualCard = cardIndex;
                }
                _eventManager(this, cardIndex, evt);
            }
            else if (_actualCard != -1)
            {
                _cards[_actualCard].SetColor(ConsoleColor.White);
                _actualCard = -1;
            }
            

        }

        private int ComputeCardTransform(int nbrCardsInRow, bool normalize, List<int> cardsMaxLineLength, int i, int maxCardHeight,
            int maxCardWidth, List<List<ColoredChar[]>> cardsValues, int NormalizedCardHeight, int nbrCardsInColumn, out int width, out int height, out int x,
            out int y)
        {
            if (normalize == false)
            {
                if (cardsMaxLineLength[i] >= maxCardHeight)
                {
                    width = cardsMaxLineLength[i] > maxCardWidth ? maxCardWidth : cardsMaxLineLength[i];
                    double ratio = width / (double) cardsMaxLineLength[i];
                    height = cardsValues[i].Count > maxCardHeight ? (int) (cardsValues[i].Count * ratio) : cardsValues[i].Count;
                }
                else
                {
                    height = cardsValues[i].Count > maxCardHeight ? maxCardHeight : cardsValues[i].Count;
                    double ratio = height / (double) cardsValues[i].Count;
                    width = cardsMaxLineLength[i] > maxCardWidth
                        ? (int) (cardsMaxLineLength[i] * ratio)
                        : cardsMaxLineLength[i];
                }
            }
            else
            {
                height = cardsValues[i].Count > NormalizedCardHeight ? NormalizedCardHeight : cardsValues[i].Count;
                double ratio = height / (double) cardsValues[i].Count;
                width = cardsMaxLineLength[i] > maxCardWidth
                    ? cardsMaxLineLength[i] * ratio > maxCardWidth ? maxCardWidth : (int) (cardsMaxLineLength[i] * ratio)
                    : cardsMaxLineLength[i];
            }


            int index = i - nbrCardsInRow * (i / nbrCardsInRow);

            x = (index + 1) * (Console.WindowWidth / nbrCardsInRow) - maxCardWidth / 2 - width / 2;

            y = (i / nbrCardsInRow + 1) * (Console.WindowHeight / nbrCardsInColumn) - maxCardHeight / 2 - height / 2;
            return width;
        }

        public void Draw()
        {
            foreach (Card card in _cards)
            {
                card.switchVisibility(20);
            }
        }

        private void LoadCards(string[] cards, List<int> cardsMaxLineLength, List<List<ColoredChar[]>> cardsValues)
        {
            for (int i = 0; i < cards.Length; i++)
            {
                List<ColoredChar[]> cardValue = new List<ColoredChar[]>();
                string path = Directory.GetCurrentDirectory() + "\\card\\";
                LoadCard(cards, cardsMaxLineLength, cardsValues, path, i, cardValue);
            }
        }

        private void LoadCard(string[] cards, List<int> cardsMaxLineLength, List<List<ColoredChar[]>> cardsValues, string path, int i, List<ColoredChar[]> cardValue)
        {
            try
            {
                var maxWidth = ReadCardFile(cards, path, i, cardValue);
                cardsMaxLineLength.Add(maxWidth);
                cardsValues.Add(cardValue);
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        private int ReadCardFile(string[] cards, string path, int i, List<ColoredChar[]> cardValue)
        {
            string line = "";
            int maxWidth = 0;
            try
            {
                StreamReader sr = new StreamReader(path + cards[i] + ".txt");

                
                while (line != null)
                {
                    line = sr.ReadLine();
                    if (line == null) break;
                    
                    string[] chars = line.Split('');
                    ColoredChar[] rslt = new ColoredChar[chars.Length-2];
                    for (int j = 1; j < rslt.Length+1; j++)
                    {
                        rslt[j-1] = new ColoredChar('X', ConsoleColor.White);
                        
                    }
                    if (line.Length > maxWidth) maxWidth = line.Length;
                    cardValue.Add(rslt);
                }
                sr.Close();
            }
            catch (FileNotFoundException e)
            {

            }catch(Exception e)
            {

            }

            return maxWidth;
        }

        public List<Card> GetCards()
        {
            return _cards;
        }
    }
}