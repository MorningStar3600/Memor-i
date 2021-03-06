using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
namespace Projet1
{
    public class Card
    {
        public string id;
        public int width { get; }
        public int height { get; }
        public int x { get; }
        public int y { get; }
        
        public int maxWidth { get; }
        public int maxHeight { get; }
        public double animationSpeed { get; set; }
        public ConsoleColor selectColor { get; }

        public List<List<ColoredChar[]>> values { get; } = new List<List<ColoredChar[]>>();
        public double animationIndex = 0;
        public List<List<ColoredChar[]>> hides { get; } = new List<List<ColoredChar[]>>();
        private List<List<ColoredChar[]>> actualAndFutureValue { get; } = new List<List<ColoredChar[]>>();

        private List<List<string>> _actualAndFutureValue { get; } = new List<List<string>>();

        private List<string> clear { get; } = new List<string>();



        public bool face { get; set; }
        public bool visible { get; set; } = true;
        public bool selected { get; set; } = false;


        public Card(string id, List<List<ColoredChar[]>> values, List<List<ColoredChar[]>> back, int x, int y, int width, int height, int maxWidth, int maxHeight, double animationSpeed, bool face = false)
        {
            this.id = id;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.maxWidth = maxWidth;
            this.maxHeight = maxHeight;
            this.animationSpeed = animationSpeed;
            this.face = face;

            for (int i = 0; i < values.Count; i++)
            {
                this.values.Add(Smooth(Resize(values[i], this.width, this.height)));
            }
            
            for (int i = 0; i < back.Count; i++)
            {
                this.hides.Add(Smooth(Resize(back[i], this.maxWidth, this.maxHeight)));
            }
            
        }

        public void Select(bool isSelected, char c = ' ', ConsoleColor color = ConsoleColor.Black)
        {
            //List<ScreenCard> list = new List<ScreenCard>();
            if (isSelected)
            {
                Surround(values, c, color);
                Surround(hides, c, color);
            }
            else
            {
                Surround(values, ' ', ConsoleColor.Black);
                Surround(hides, ' ', ConsoleColor.Black);
            }
        }

        private void Surround(List<List<ColoredChar[]>> list, char c, ConsoleColor color)
        {
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list[i].Count;j++)
                {
                    for (int k = 0; k < list[i][j].Length; k++)
                    {
                        if (j == 0 || j == list[i].Count - 1 || k == 0 || k == list[i][j].Length - 1)
                        {
                            list[i][j][k].color = color;
                            list[i][j][k].c = c;
                        }

                    }
                }
            }
        }

        public void Fade(bool fade)
        {
            if (fade)
            {
                ChangeColor(values, -1);
                ChangeColor(hides,-1);
            }
            else
            {
                ChangeColor(values, 1);
                ChangeColor(hides,1);
            }
        }

        private void ChangeColor(List<List<ColoredChar[]>> list, int howToChange)
        {
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list[i].Count;j++)
                {
                    for (int k = 0; k < list[i][j].Length; k++)
                    {
                        if (j == 0 || j == list[i].Count - 1 || k == 0 || k == list[i][j].Length - 1)
                        {
                            list[i][j][k].color += howToChange;
                            list[i][j][k].bColor += howToChange;
                        }

                    }
                }
            }
        }

        public bool IsInCard(int x, int y)
        {
            if (x >= this.x && x <= this.x + this.width && y >= this.y && y <= this.y + this.height) return true;
            return false;
        }

        public List<ColoredChar[]> GetActualValue()
        {
            if (_actualAndFutureValue.Count > 0)
            {
                List<ColoredChar[]> rslt = actualAndFutureValue[0];
                actualAndFutureValue.RemoveAt(0);
                return rslt;
            }

            if (visible)
            {
                if (face)
                {
                    
                    animationIndex += animationSpeed;
                    if (animationIndex >= values.Count) animationIndex = 0;
                    return values[(int)animationIndex];
                }
                else
                {
                    animationIndex += animationSpeed;
                    if (animationIndex >= hides.Count) animationIndex = 0;
                    return hides[(int)animationIndex];
                }
            }
            else
            {
                return null;
            }
           
        }
        
        public void NextAnimation()
        {
            if (visible)
            {
                if (face)
                {
                    animationIndex += 1;
                    if (animationIndex >= values.Count) animationIndex = 0;
                }
                else
                {
                    animationIndex += 1;
                    if (animationIndex >= hides.Count) animationIndex = 0;
                }
            }
        }
        
        public void PrevAnimation()
        {
            if (visible)
            {
                if (face)
                {
                    animationIndex -= 1;
                    if (animationIndex < 0) animationIndex = values.Count - 1;
                }
                else
                {
                    animationIndex -= 1;
                    if (animationIndex < 0) animationIndex = hides.Count - 1;
                }
            }
        }
        //public DrawItem Draw(DrawItem di = null)
        //{
        //    List<ScreenCard> list = new List<ScreenCard>();
        //    list.Add(new ScreenCard(this.color, x, y, _value, 1));
        //    DrawItem d = new DrawItem(list, di);
        //    if (_value != null) Draws.toDraw.Add(d);
        //    return d;
        //}
        //public void Clear()
        //{
        //    List<ScreenCard> list = new List<ScreenCard>();
        //    list.Add(new ScreenCard(this.color, x, y, _value, 0));
        //    if (_value != null) Draws.toDraw.Add(new DrawItem(list, null));
        //}

        /*public DrawItem switchCard(int speed, DrawItem di = null)
        {
            List<ScreenCard> list = new List<ScreenCard>();
            if (this._value != null)
            {
                List<string> toRotate = rectoVerso == true ? this._value : this._hide;
                for (int i = 0; i < speed; i++)
                {
                    float rotation = (i + 1) / (float)speed;
                    List<string> rslt = Rotate(Smooth(Resize(toRotate, (this.width - i * this.width / speed), this.height)), (int)(rotation*8));
                    ScreenCard sc = new ScreenCard(this.color, x, y, rslt, 1);
                    list.Add(sc);
                }

                if (this.rectoVerso)
                {
                    toRotate = this._hide;
                    this.rectoVerso = false;
                }
                else
                {
                    toRotate = this._value;
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
            
        }*/

        public void switchVisibility(int speed)
        {
            if (this.visible)
            {
                this.visible = false;
                List<ColoredChar[]> toDraw = this.values[(int)animationIndex];
                for (int i = 0; i < speed; i++)
                {
                    List<ColoredChar[]> rslt = Smooth(Resize(Rotate(Smooth(Resize(toDraw, this.width - i * this.width / speed, this.height)), i + 1), this.width, this.height));
                    actualAndFutureValue.Add(rslt);
                }
                List<ColoredChar[]> rslt_ = this.values[(int)animationIndex];
                actualAndFutureValue.Add(rslt_);

            }
            else
            {
                this.visible = true;
                List<ColoredChar[]> toDraw = this.values[(int)animationIndex];

                for (int i = speed - 1; i >= 0; i--)
                {
                    List<ColoredChar[]> rslt = Smooth(Resize(Rotate(Smooth(Resize(toDraw, this.width - i * this.width / speed, this.height)), i + 1, false), this.width, this.height));
                    actualAndFutureValue.Add(rslt);
                }
                List<ColoredChar[]> rslt_ = Rotate(Smooth(Resize(toDraw, this.width, this.height)), 0, false);
                actualAndFutureValue.Add(rslt_);
            }
            
            /*Console.WriteLine("x:"+x+" y:"+y + "w:"+width+" h:"+height);
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
            return d;*/
        }

        public static List<ColoredChar[]> Rotate(List<ColoredChar[]> card, int rotation, bool clockwise = true)
        {
            int maxWidth = GetWidth(card);

            ColoredChar[,] tab = new ColoredChar[card.Count,maxWidth];

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
                    finalY = finalY <= tab.GetLength(0) - 1 ? finalY : tab.GetLength(0)-1;
                    int finalX = x;
                    //Debug.WriteLine("hhhhhhhhhhhhhhhhhhhhhhhhhhh");
                    //Debug.Write(tab.GetLength(0));
                    //Console.ReadKey();
                    //Console.WriteLine(tab[finalY, 0]);
                    tab[finalY, finalX] = card[i][j].c == ' ' ? new ColoredChar('é', card[i][j].color, card[i][j].bColor) : new ColoredChar(card[i][j].c, card[i][j].color, card[i][j].bColor);    
                    x++;    
                }
                y++;
            }
            List<ColoredChar[]> rslt = new List<ColoredChar[]>();
            for (int i = 0; i < tab.GetLength(0); i++)
            {
                ColoredChar[] s = new ColoredChar[tab.GetLength(1)];
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    
                    if (tab[i,j] == null)
                    {
                        s[j] = new ColoredChar(' ', ConsoleColor.White);
                    }
                    else
                    {
                        s[j] = tab[i, j].c == 'é' ? new ColoredChar(' ', tab[i, j].color, tab[i,j].bColor) : new ColoredChar(tab[i, j].c, tab[i, j].color, tab[i,j].bColor);
                    }

                    
                }
                rslt.Add(s);
            }
            return rslt;
        }

        public static ColoredChar[,] Resize(List<ColoredChar[]> card, int width, int height)
        {
            float maxWidth = GetWidth(card);
            float maxHeight = card.Count;
            ColoredChar[,] tab = new ColoredChar[height,width];
            if (card.Count > 0 && width > 0 && height > 0)
            {
                for (int i = 0; i < card.Count; i++)
                {
                    for (int j = 0; j < card[i].Length; j++)
                    {
                        //Console.Write(card[i][j]);
                        //Console.ReadKey();
                        if (card[i][j] != null)
                        {
                            tab[(int)(i / maxHeight * height), (int)(j / maxWidth * width)] =
                            card[i][j].c == ' ' ? new ColoredChar('é', card[i][j].color, card[i][j].bColor) : new ColoredChar(card[i][j].c, card[i][j].color, card[i][j].bColor);
                        }
                        else
                        {
                            Console.Write("fuck");
                        }
                        
                    }
                }

                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        if (tab[i,j] == null)
                        {
                            tab[i, j] = new ColoredChar('é', ConsoleColor.White);
                        }
                    }
                }


            }
            

            

            return tab;
        }

        public static List<ColoredChar[]> Smooth(ColoredChar[,] tab)
        {
            //Console.Write(tab[0,0]);
            //Console.ReadKey();
            Random rdm = new Random();

            for (int i = 0; i < tab.GetLength(0); i++)
            {
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    if (tab[i,j].c== '\0')
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


            List<ColoredChar[]> rslt = new List<ColoredChar[]>();

            for (int i = 0; i < tab.GetLength(0); i++)
            {
                ColoredChar[] s = new ColoredChar[tab.GetLength(1)];
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    s[j] = tab[i, j].c == 'é' ? new ColoredChar(' ', tab[i,j].color, tab[i,j].bColor) : new ColoredChar(tab[i,j].c, tab[i,j].color, tab[i,j].bColor);
                }
                rslt.Add(s);
            }
            return rslt;
        }

        public static int GetWidth(List<ColoredChar[]> card)
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