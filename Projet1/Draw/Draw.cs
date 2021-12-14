using System;
using System.Collections.Generic;
using System.Threading;

namespace Projet1
{
    class Draws
    {
        static public List<Object> toDraw { get; set; } = new ();
        static private readonly Thread _thread;
        private static char[,] _screenBuffer;
        private static short[,] _screenBufferColor;
        private static short[,] _screenBufferBColor;
        private static FastConsole.CharInfo[] _buffer;


        private static int width;
        private static int height;
        static Draws()
        {
            _thread = new Thread(ReDraw);
        }
        public static void Run()
        {
            _thread.Start();
        }

        public static void Stop()
        {
            _thread.Abort();
            toDraw.Clear();
        }
        
        public static void Update()
        {
            width = Console.WindowWidth;
            height = Console.WindowHeight;
            _screenBuffer = new char[height,width];
            _screenBufferColor = new short[height,width];
            _screenBufferBColor = new short[height,width];
            _buffer = new FastConsole.CharInfo[width * height];
        }

        private static void ReDraw()
        {
            bool draw = false;
            long milliseconds;
            long diff;
            width = Console.WindowWidth;
            height = Console.WindowHeight;
            _screenBuffer = new char[height, width];
            _screenBufferColor = new short[height, width];
            _screenBufferBColor = new short[height, width];
            _buffer = new FastConsole.CharInfo[height * width];
            while (true)
            {
                milliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                
                draw = false;
                for (int i = 0; i < toDraw.Count; i++)
                {
                    if (toDraw[i] is Card c)
                    {
                        if (AppendToActualScreenBuffer(c))
                        {
                            draw = true;
                        }
                    }else if (toDraw[i] is AnimCard ac)
                    {
                        //(_screenBuffer, _screenBufferColor, _screenBufferBColor) = ac.GetValue();
                        ForceDrawBuffer(ac.GetBuffer());
                        draw = false;
                        break;
                    }else if (toDraw[i] is ScoreTracker st)
                    {
                        if (AppendToActualScreenBuffer(st))
                        {
                            draw = true;
                        }
                    }else if (toDraw[i] is UpdatedText ut)
                    {
                        if (AppendToActualScreenBuffer(ut))
                        {
                            draw = true;
                        }
                    }
                }
                
                if (draw)
                {
                    Draw();
                }
                
                diff = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                diff -= milliseconds;
                //Console.Write("\r" + diff);
                if (diff < 18)
                {
                   //Thread.Sleep(20-(int)diff); 
                }
            }
        }

        private static bool AppendToActualScreenBuffer(Card c)
        {
            int x = c.x;
            int y = c.y;
            
            List<ColoredChar[]> lines = c.GetActualValue();
            if (lines is {Count: > 0})
            {
                char[,] value = new char[lines.Count,Card.GetWidth(lines)];
                int k = 0;
                int l = 0;

                int length0 = value.GetLength(0);
                int length1 = value.GetLength(1);
                for (int i = 0; i < length0; i++)
                {
                    l = y + i;
                    for (int j = 0; j < length1; j++)
                    {
                        k = x + j;
                        
                        if (k >= 0 && k < width && l >= 0 && l < height && lines[i][j] != null)
                        {
                            _screenBuffer[l, k] = lines[i][j].c;
                            _screenBufferColor[l, k] = (short)lines[i][j].color;
                            _screenBufferBColor[l, k] = (short)lines[i][j].bColor;
                            
                        }
                    }
                }
                return true;
            }
            return false;
        }
        
        private static bool AppendToActualScreenBuffer(ScoreTracker sc)
        {
            int x = sc.x;
            int y = sc.y;
            
            ColoredChar[,] lines = sc.GetScore();
            if (lines.GetLength(0)>0)
            {
                char[,] value = new char[lines.GetLength(0),lines.GetLength(1)];
                int k = 0;
                int l = 0;

                int length0 = value.GetLength(0);
                int length1 = value.GetLength(1);
                for (int i = 0; i < length0; i++)
                {
                    l = y + i;
                    for (int j = 0; j < length1; j++)
                    {
                        k = x + j;
                        
                        if (k >= 0 && k < width && l >= 0 && l < height && lines[i,j] != null)
                        {
                            _screenBuffer[l, k] = lines[i,j].c;
                            _screenBufferColor[l, k] = (short)lines[i,j].color;
                            _screenBufferBColor[l, k] = (short)lines[i,j].bColor;
                            
                        }
                    }
                }
                return true;
            }
            return false;
        }
        
        private static bool AppendToActualScreenBuffer(UpdatedText ut)
        {
            int x, y;

            (x, y) = ut.GetPosition();
            
            ColoredChar[] lines = ut.GetValue();
            if (lines.Length>0)
            {
                char[,] value = new char[1,lines.Length];
                int k = 0;

                int length0 = value.Length;
                for (int i = 0; i < length0; i++)
                {
                    k = x + i;
                    if (k >= 0 && k < width && y >= 0 && y < height && lines[i] != null)
                    {
                        _screenBuffer[y, k] = lines[i].c;
                        _screenBufferColor[y, k] = (short)lines[i].color;
                        _screenBufferBColor[y, k] = (short)lines[i].bColor;
                            
                    }
                }
                return true;
            }
            return false;
        }

        private static void Draw()
        {
            int length0 = _screenBuffer.GetLength(0);
            int length1 = _screenBuffer.GetLength(1);
            
            int index = 0;

            
            for (int i = 0; i < length0; i++)
            {
                index = i * width;
                for (int j = 0; j < length1; j++)
                {
                    _buffer[index].Char.UnicodeChar = _screenBuffer[i, j];
                    _buffer[index].Attributes = (short) (_screenBufferColor[i,j] | (_screenBufferBColor[i,j]<<4));
                    index++;
                }
            }
            FastConsole.Write(_buffer, 0, 0, length1, length0);
        }

        private static void ForceDrawBuffer(FastConsole.CharInfo[] buffer)
        {
            FastConsole.Write(buffer, 0, 0, width,height);
        }

        public static void Clear()
        {
            _screenBuffer = new char[height, width];
            _screenBufferColor = new short[height, width];
            _screenBufferBColor = new short[height, width];
            _buffer = new FastConsole.CharInfo[width * height];
            FastConsole.Write(_buffer, 0, 0, width, height);
        }
    }
}