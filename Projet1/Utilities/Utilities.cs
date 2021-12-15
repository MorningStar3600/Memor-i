using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet1
{
    class Utilities
    {
        public static void DrawColoredCharArray(List<ColoredChar[]> arr)
        {
            for (int i = 0; i < arr.Count; i++)
            {
                for (int j = 0; j < arr[i].Length; j++)
                {
                    if (arr[i][j] == null)
                    {
                        Console.Write("_");
                    }
                    else
                    {
                        Console.Write(arr[i][j].c);
                    }
                }
                Console.WriteLine();
            }
        }

        public static void DrawColoredCharArray(ColoredChar[,] arr)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (arr[i,j] == null)
                    {
                        Console.Write("_");
                    }
                    else
                    {
                        Console.Write(arr[i,j].c);
                    }
                }
                Console.WriteLine();
            }
        }
        public static void DrawColoredCharArray(ColoredChar[] arr)
        {
            for (int j = 0; j < arr.Length; j++)
            {
                if (arr[j] == null)
                {
                    Console.Write("_");
                }
                else
                {
                    Console.Write(arr[j].c);
                }
            }
        }

        public static void DrawCenteredText(string text)
        {
            Console.SetCursorPosition(Console.WindowWidth/ 2 - text.Length / 2, Console.WindowHeight/2);
            Console.Write(text);
        }
        
    }
}
