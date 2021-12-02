using System;

namespace Projet1
{
    class ProgressBar
    {
        private int _x;
        private int _y;
        private int _nbrElmts;
        private int _actualElmt = 0;
        public ProgressBar(int x, int y, int elmts)
        {
            this._x = x;
            this._y = y;
            this._nbrElmts = elmts;
            Draw();
        }
        
        public void Draw()
        {
            
            for (int i = 0; i < 3; i++)
            {
                Console.SetCursorPosition(_x-GetWidth()/2, _y-1+i);
                string line = "X";
                for (int j = 1; j <= _nbrElmts; j++)
                {
                    if (i == 0 || i == 2)
                    {
                        line += "X";
                    }
                    else
                    {
                        if (j <= _actualElmt)
                        {
                            line += "=";
                        }
                        else
                        {
                            line += " ";
                        }
                    }
                }
                line += "X";
                Console.Write(line);
            }
        }
        
        public void Update()
        {
            if (_actualElmt < _nbrElmts)_actualElmt++;
            Draw();
        }
        
        public void Reset()
        {
            _actualElmt = 0;
        }
        
        private int GetWidth()
        {
            return _nbrElmts + 2;
        }
    }
}