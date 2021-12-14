using System;

namespace Projet1
{
    class UpdatedText
    {
        private string value;
        private int x;
        private int y;
        private ConsoleColor color;
        private ConsoleColor bColor;
        public UpdatedText(string value, int x, int y)
        {
            this.value = value;
            this.x = x;
            this.y = y;
            this.color = ConsoleColor.White;
            this.bColor = ConsoleColor.Black;
        }
        
        public UpdatedText(string value, int x, int y, ConsoleColor c)
        {
            this.value = value;
            this.x = x;
            this.y = y;
            this.color = c;
            this.bColor = ConsoleColor.Black;
        }
        
        public UpdatedText(string value, int x, int y, ConsoleColor c, ConsoleColor bC)
        {
            this.value = value;
            this.x = x;
            this.y = y;
            this.color = c;
            this.bColor = bC;
        }

        public ColoredChar[] GetValue()
        {
            ColoredChar[] result = new ColoredChar[value.Length];
            for (int i = 0; i < value.Length; i++)
            {
                result[i] = new ColoredChar(value[i], color, bColor);
            }
            return result;
        }
        
        public (int, int) GetPosition()
        {
            return (x, y);
        }
    }
}