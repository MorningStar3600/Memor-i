using System;

namespace Projet1
{
    class ScoreTracker
    {
        private Game _game;
        public int x;
        public int y;
        public int height;
        public int width;
        
        public ScoreTracker(Game g, int x, int y, int height)
        {
            _game = g;
            this.x = x;
            this.y = y;
            this.height = height;
        }

        public ColoredChar[,] GetScore()
        {
            width = _game.GetNumbPlayers()*4;
            
            ColoredChar[,] rslt = new ColoredChar[height*2+2,width*4];

            int maxScore = _game.maxScore;

            for (int i = 0; i < width/4; i++)
            {
                char playerChar = _game._players[i].character;
                ConsoleColor playerColor = _game._players[i].color;
                
                if (Score.GetScore(_game.IdGame, _game._players[i]) >= 0)
                {
                    int scale = (int)((double)Score.GetScore(_game.IdGame, _game._players[i]) / maxScore*height);
                    if (scale > height) scale = height;
                    for (int j = 0; j < scale; j++)
                    {
                        rslt[height - j, i*4] = new ColoredChar(playerChar, playerColor);
                        rslt[height - j, i*4+1] = new ColoredChar(playerChar, playerColor);
                        rslt[height - j, i*4+2] = new ColoredChar(playerChar, playerColor);
                        //rslt[height - j, i*4+3] = new ColoredChar('|', ConsoleColor.White);
                    }
                    for (int j = scale; j < height; j++)
                    {
                        rslt[height - j, i*4] = new ColoredChar(' ', ConsoleColor.Black);
                        rslt[height - j, i*4+1] = new ColoredChar(' ', ConsoleColor.Black);
                        rslt[height - j, i*4+2] = new ColoredChar(' ', ConsoleColor.Black);
                        //rslt[height - j, i*4+3] = new ColoredChar('|', ConsoleColor.White);
                    }
                }
                else
                {
                    int scale = -(int)((double)Score.GetScore(_game.IdGame, _game._players[i]) / maxScore*height);
                    if (scale > height) scale = height;
                    for (int j = 0; j < scale; j++)
                    {
                        rslt[height + j, i*4] = new ColoredChar(playerChar, playerColor);
                        rslt[height + j, i*4+1] = new ColoredChar(playerChar, playerColor);
                        rslt[height + j, i*4+2] = new ColoredChar(playerChar, playerColor);
                        
                    }
                    for (int j = scale; j < height; j++)
                    {
                        rslt[height + j, i*4] = new ColoredChar(' ', ConsoleColor.Black);
                        rslt[height + j, i*4+1] = new ColoredChar(' ', ConsoleColor.Black);
                        rslt[height + j, i * 4 + 2] = new ColoredChar(' ', ConsoleColor.Black);
                    }
                }
            }

            
            return rslt;
        }
    }
}