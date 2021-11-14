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
        public int x { get; }
        public int y { get; }
        public List<string> value { get; } = new List<string>();
        public List<string> hide { get; } = new List<string>();

        public bool rectoVerso { get; set; } = true;
        public bool visible { get; set; }
        public ConsoleColor color { get; set; } = ConsoleColor.White;


        public Card(int id, List<string> value, int x, int y, int width, int height)
        {
            this.id = id;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.value = value;

            this.value = Smooth(Resize(this.value, this.width, this.height));

            for (int i = 0; i < this.height; i++)
            {
                string tab = "";
                for (int j = 0; j < this.width; j++)
                {
                    if ((j == 0 || j == this.width - 1) || (i == 0 || i == this.height - 1)) tab += 'X';
                    else tab += ' ';
                }
                this.hide.Add(tab);
            }
        }

        public void SetColor(ConsoleColor c)
        {
            List<ScreenCard> list = new List<ScreenCard>();
            this.color = c;
            if (this.rectoVerso == true)
            {
                ScreenCard s = new ScreenCard(this.color, this.x, this.y, value, 1);
                list.Add(s);
            }

            DrawItem d = new DrawItem(list, null);
            Draws.toDraw.Add(d);
        }

        public bool IsInCard(int x, int y)
        {
            if (x >= this.x && x <= this.x + this.width && y >= this.y && y <= this.y + this.height) return true;
            return false;
        }
        public DrawItem Draw(DrawItem di = null)
        {
            List<ScreenCard> list = new List<ScreenCard>();
            list.Add(new ScreenCard(this.color, x, y, this.value, 1));
            DrawItem d = new DrawItem(list, di);
            if (this.value != null) Draws.toDraw.Add(d);
            return d;
        }
        public void Clear()
        {
            List<ScreenCard> list = new List<ScreenCard>();
            list.Add(new ScreenCard(this.color, x, y, this.value, 0));
            if (this.value != null) Draws.toDraw.Add(new DrawItem(list, null));
        }

        public DrawItem switchCard(int speed, DrawItem di = null)
        {
            List<ScreenCard> list = new List<ScreenCard>();
            if (this.value != null)
            {
                List<string> toRotate = rectoVerso == true ? this.value : this.hide;
                for (int i = 0; i < speed; i++)
                {
                    float rotation = (i + 1) / (float)speed;
                    List<string> rslt = Rotate(Smooth(Resize(toRotate, (this.width - i * this.width / speed), this.height)), (int)(rotation*8));
                    int width = GetWidth(rslt);
                    int height = rslt.Count;
                    float factor = (float)height / (float)this.height;
                    //rslt = Smooth(Resize(rslt, (int)(width * factor), this.height));
                    ScreenCard sc = new ScreenCard(this.color, x, y, rslt, 1);
                    list.Add(sc);
                }

                if (this.rectoVerso)
                {
                    toRotate = this.hide;
                    this.rectoVerso = false;
                }
                else
                {
                    toRotate = this.value;
                    this.rectoVerso = true;
                }

                for (int i = speed -1 ; i >= 0; i--)
                {
                    float rotation = (i + 1) / (float)speed;
                    List<string> rslt = Rotate(Smooth(Resize(toRotate, this.width - i * this.width / speed, this.height)), (int)(rotation*8), false);
                    ScreenCard sc = new ScreenCard(this.color, x, y, rslt, 1);
                    list.Add(sc);
                }
                List<string> rslt_ = Rotate(Smooth(Resize(toRotate, this.width, this.height)), 0, false);
                ScreenCard s = new ScreenCard(this.color, x, y, rslt_, 1);
                list.Add(s);

                DrawItem d = new DrawItem(list, di);
                Draws.toDraw.Add(d);
                return d;
            }
            return null;
            
        }

        public DrawItem switchVisibility(int speed, DrawItem di = null)
        {
            Console.WriteLine("x:"+x+" y:"+y + "w:"+width+" h:"+height);
            List<ScreenCard> list = new List<ScreenCard>();
            if (this.visible)
            {
                this.visible = false;
                List<string> toDraw = this.value;
                for (int i = 0; i < speed; i++)
                {
                    List<string> rslt = Smooth(Resize(Rotate(Smooth(Resize(toDraw, this.width - i * this.width / speed, this.height)), i + 1), this.width, this.height));
                    ScreenCard sc = new ScreenCard(this.color, x, y, rslt, 1);
                    list.Add(sc);
                }
                List<string> rslt_ = this.value;
                ScreenCard s = new ScreenCard(this.color, x, y, rslt_, 0);
                list.Add(s);

            }
            else
            {
                this.visible = true;
                List<string> toDraw = this.value;

                for (int i = speed - 1; i >= 0; i--)
                {
                    List<string> rslt = Smooth(Resize(Rotate(Smooth(Resize(toDraw, this.width - i * this.width / speed, this.height)), i + 1, false), this.width, this.height));
                    ScreenCard sc = new ScreenCard(this.color, x, y, rslt, 1);
                    list.Add(sc);
                }
                List<string> rslt_ = Rotate(Smooth(Resize(toDraw, this.width, this.height)), 0, false);
                ScreenCard s = new ScreenCard(this.color, x, y, rslt_, 1);
                list.Add(s);
            }
            DrawItem d = new DrawItem(list, di);
            Draws.toDraw.Add(d);
            return d;
        }

        public static List<string> Rotate(List<string> card, int rotation, bool clockwise = true)
        {
            int maxWidth = GetWidth(card);

            char[,] tab = new char[card.Count,maxWidth];

            int y = rotation;
            int x;

            for (int i = 0; i < card.Count; i++)
            {
                x = 0;
                for (int j = 0; j < card[i].Length; j++)
                {
                    float coefY = -(i - (float)card.Count / 2) / ((float)card.Count / 2);
                    float coefX = j/(float)card[i].Length;
                    int finalY = (int)(i+(rotation * coefY * coefX));
                    int finalX = x;
                    tab[finalY, finalX] = card[i][j] == ' ' ? 'é' : card[i][j];    
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
            if (card.Count > 0 && width > 0 && height > 0)
            {
                for (int i = 0; i < card.Count; i++)
                {
                    for (int j = 0; j < card[i].Length; j++)
                    {
                        tab[(int)((float)i / maxHeight * height), (int)((float)j / maxWidth * width)] = card[i][j] == ' ' ? 'é' : card[i][j];
                    }
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