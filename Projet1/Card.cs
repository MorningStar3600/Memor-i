using System;
using System.Collections.Generic;
using System.IO;
namespace Projet1
{
    class Card
    {
        private int id;
        public int width { get; }
        public int height { get; }
        public List<string> value { get; } = new List<string>();
        public List<string> hide { get; } = new List<string>();

        public bool visible { get; set; } = true;


    public Card(int id, string name)
        {
            this.id = id;
            string path = Directory.GetCurrentDirectory() + "\\card\\";
            try
            {
                Console.WriteLine(path);
                StreamReader sr = new StreamReader(path+name+".txt");

                string line = "";
                int nbLine = 0;
                int maxWidth = 0;
                while (line != null)
                {
                    line = sr.ReadLine();
                    if (line != null)
                    {
                        if (line.Length > maxWidth) maxWidth = line.Length;
                        this.value.Add(line);
                        nbLine++;
                    }
                }
                this.width = maxWidth;
                this.height = nbLine;

                for (int i = 0; i < nbLine; i++)
                {
                    string tab = "";
                    for (int j = 0; j < maxWidth; j++)
                    {
                        if ((j == 0 || j == maxWidth - 1) || (i == 0 || i == nbLine - 1)) tab += 'X';
                        else tab += ' ';
                    }
                    this.hide.Add(tab);
                }

                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public DrawItem Draw(int x, int y, DrawItem di = null)
        {
            List<ScreenCard> list = new List<ScreenCard>();
            list.Add(new ScreenCard(x, y, this.value, 1));
            DrawItem d = new DrawItem(list, di);
            if (this.value != null) Draws.toDraw.Add(d);
            return d;
        }
        public void Clear(int x, int y)
        {
            List<ScreenCard> list = new List<ScreenCard>();
            list.Add(new ScreenCard(x, y, this.value, 0));
            if (this.value != null) Draws.toDraw.Add(new DrawItem(list, null));
        }

        public void switchCard(int x, int y, int speed, DrawItem di = null)
        {
            List<ScreenCard> list = new List<ScreenCard>();
            if (this.value != null)
            {
                List<string> toRotate = visible == true ? this.value : this.hide;
                for (int i = 0; i < speed; i++)
                { 
                    List<string> rslt = Rotate(Smooth(Resize(toRotate, this.width - i*this.width/speed,this.height)), i+1);
                    ScreenCard sc = new ScreenCard(x, y, rslt, 1);
                    list.Add(sc);
                }

                if (this.visible == true)
                {
                    toRotate = this.hide;
                    this.visible = false;
                }
                else
                {
                    toRotate = this.value;
                    this.visible = true;
                }

                for (int i = speed -1 ; i >= 0; i--)
                {
                    List<string> rslt = Rotate(Smooth(Resize(toRotate, this.width - i * this.width / speed, this.height)), i + 1, false);
                    ScreenCard sc = new ScreenCard(x, y, rslt, 1);
                    list.Add(sc);
                }
                List<string> rslts = Rotate(Smooth(Resize(toRotate, this.width, this.height)), 0, false);
                ScreenCard s = new ScreenCard(x, y, rslts, 1);
                list.Add(s);

                Draws.toDraw.Add(new DrawItem(list, di));

            }
            
        }

        public static List<string> Rotate(List<string> card, int rotation, bool clockwise = true)
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
                    coefX = clockwise == true ? coefX : -coefX;
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