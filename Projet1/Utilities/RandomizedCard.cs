using System;
using System.Collections.Generic;

namespace Projet1
{
    public static class RandomizedCard
    {
        private static readonly string[] PossibleCards = {"2", "3", "4", "5", "6", "7", "8", "9", "10", "11"};
        public static string[] GetCardPair(int nbrPair)
        {
            Random rdm = new Random();
            string[] toDispatch = new string[nbrPair];
            for (int i =0; i < nbrPair; i++)
            {
                bool isUnique;
                int value;
                do
                {
                    value = rdm.Next(0, PossibleCards.Length);
                    isUnique = true;
                    for (int j = 0; j < i; j++)
                    {
                        if (toDispatch[j] == PossibleCards[value])
                        {
                            isUnique = false;
                            break;
                        }
                    }
                } while (!isUnique); 
                toDispatch[i] = PossibleCards[value];
            }
            
            
            string[] cards = new string[2*nbrPair];

            for (int i = 0; i < cards.Length; i++)
            {
                int pos;
                do
                {
                    pos = rdm.Next(0, cards.Length);
                }while (cards[pos] != null);

                cards[pos] = toDispatch[i/2];
            }
            
            return cards;
        }
    }
}