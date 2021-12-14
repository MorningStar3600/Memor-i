using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet1
{
    public class ColoredChar
    {
        public char c;
        public static int nbr = 0;
        public ConsoleColor color;
        public ConsoleColor bColor;

        public ColoredChar(char c, ConsoleColor color)
        {
            this.c = c;
            this.color = color;
            this.bColor = ConsoleColor.Black;
        }

        public ColoredChar(char c, ConsoleColor color, ConsoleColor bColor)
        {
            this.c = c;
            this.color = color;
            this.bColor = bColor;
        }

        public override string ToString()
        {
            return "[ColoredChar]" ;
        }

        public static ConsoleColor GetColor(string value, string color)
        {
            int frstValue = int.Parse(value);
            int scdValue = int.Parse(color) % 10;
            

            ConsoleColor toReturn;
            switch (scdValue)
            {
                case 0:
                    toReturn = frstValue == 0 ? ConsoleColor.Black : ConsoleColor.DarkGray;
                    break;

                case 1:
                    toReturn = frstValue == 0 ? ConsoleColor.DarkRed : ConsoleColor.Red;
                    break;

                case 2:
                    toReturn = frstValue == 0 ? ConsoleColor.DarkGreen : ConsoleColor.Green;
                    break;

                case 3:
                    toReturn = frstValue == 0 ? ConsoleColor.DarkYellow : ConsoleColor.Yellow;
                    break;

                case 4:
                    toReturn = frstValue == 0 ? ConsoleColor.DarkBlue : ConsoleColor.Blue;
                    break;

                case 5:
                    toReturn = frstValue == 0 ? ConsoleColor.DarkMagenta : ConsoleColor.Magenta;
                    break;

                case 6:
                    toReturn = frstValue == 0 ? ConsoleColor.DarkCyan : ConsoleColor.Cyan;
                    break;

                case 7:
                    toReturn = frstValue == 0 ? ConsoleColor.Gray : ConsoleColor.White;
                    break;

                default:
                    toReturn = ConsoleColor.Black;
                    break;
            }
            

            return toReturn;
        }
    }
}
