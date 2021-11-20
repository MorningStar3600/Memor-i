using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet1
{
    class ColoredChar
    {
        public char c;
        public ConsoleColor color;

        public ColoredChar(char c, ConsoleColor color)
        {
            this.c = c;
            this.color = color;
        }

        public override string ToString()
        {
            return "[ColoredChar]" ;
        }
    }
}
