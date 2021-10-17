using System;
using System.Collections.Generic;
using System.IO;
namespace Projet1
{
    public class Card
    {
        private int id;
        public int width { get; }
        public int height { get; }
        public List<string> value  {get;}


    public Card(int id, string name)
        {
            this.value = new List<string>();
            this.id = id;
            string path = Directory.GetCurrentDirectory() + "\\card\\";
            try
            {
                Console.WriteLine(path);
                StreamReader sr = new StreamReader(path+name+".txt");

                string line = "";
                int nbLine = 0;
                int maxLength = 0;
                while (line != null)
                {
                    line = sr.ReadLine();
                    if (line != null)
                    {
                        if (line.Length > maxLength) maxLength = line.Length;
                        this.value.Add(line);
                        nbLine++;
                    }
                }
                this.width = maxLength;
                this.height = nbLine;

                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void Draw(int size, int startX, int startY)
        {
            int y = startY;

            for (int i = 0; i < this.value.Count; i++)
            {
                Console.SetCursorPosition(startX, y);
                if (i%size != 0)
                {   
                    for (int j = 0; j < this.value[i].Length; j++)
                    {
                        if (j%size != 0)
                        {
                            Console.Write(this.value[i][j]);
                        }
                    }
                    Console.WriteLine();
                    y++;
                } 
            }
        }

        public static void Draw(List<string> card, int startX, int startY)
        {
            for (int i = 0; i < card.Count; i++)
            {
                Console.SetCursorPosition(startX, startY + i);
                Console.WriteLine(card[i]);
            }
        }

        public static void Clear(List<string> card, int startX, int startY)
        {
            int maxWidth = GetWidth(card);
            for (int i = 0; i < card.Count; i++)
            {
                Console.SetCursorPosition(startX, startY + i);
                char[] c = new char[maxWidth];
                for (int j = 0; j < maxWidth; j++)
                {
                    c[j] = ' ';
                }
                Console.Write(c);

            }
        }

        public static List<string> Rotate(List<string> card, int rotation)
        {
            int maxWidth = GetWidth(card);

            char[,] tab = new char[card.Count+2*rotation,maxWidth];

            int y = rotation;
            int x;

            for (int i = 0; i < card.Count; i++)
            {
                x = 0;
                for (int j = 0; j < card[i].Length; j++)
                {
                    float coefY = ((float)i - (float)card.Count / 2) / ((float)card.Count / 2);
                    float coefX = -((float)j - (float)card[i].Length / 2) / ((float)card[i].Length / 2);
                    tab[(int)(y + (rotation * coefY * coefX)), x] = card[i][j] == ' ' ? 'é' : card[i][j];    
                    x++;    
                }
                y++;
            }
            List<string> rslt = new List<string>();

            for (int i = 0; i < tab.GetLength(0); i++)
            {
                string s = "";
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    s += tab[i, j] == 'é' ? ' ' : tab[i, j];
                }
                rslt.Add(s);
            }
            return rslt;
        }

        public static char[,] Resize(List<string> card, int width, int height)
        {
            int maxWidth = GetWidth(card);
            int maxHeight = card.Count;
            char[,] tab = new char[height,width];
            for (int i = 0; i < card.Count; i++)
            {
                for (int j = 0; j < card[i].Length; j++)
                {
                    tab[(int)((float)i / maxHeight * height), (int)((float)j / maxWidth * width)] = card[i][j] == ' ' ? 'é' : card[i][j];
                }
            }

            

            return tab;
        }

        public static List<string> Smooth(char[,] tab)
        {
            Random rdm = new Random();

            for (int i = 0; i < tab.GetLength(0); i++)
            {
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    if (tab[i,j]== '\0')
                    {
                        int value = rdm.Next(0, 2);
                        if (i > 0 && i < tab.GetLength(0)-1)
                        {
                            tab[i, j] = value == 0 ? tab[i - 1, j] : tab[i + 1, j]; 
                        }
                        else if (i == 0)
                        {
                            tab[i, j] = tab[i + 1, j];
                        }
                        else
                        {
                            tab[i, j] = tab[i - 1, j];
                        }
                        
                    }
                }
            }


            List<string> rslt = new List<string>();

            for (int i = 0; i < tab.GetLength(0); i++)
            {
                string s = "";
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    s += tab[i, j] == 'é' ? ' ' : tab[i,j];
                }
                rslt.Add(s);
            }
            return rslt;
        }

        public static int GetWidth(List<string> card)
        {
            int lengthMax = 0;
            for (int i = 0; i < card.Count; i++)
            {
                if (card[i].Length > lengthMax) lengthMax = card[i].Length;
            }

            return lengthMax;
        }
    }
}