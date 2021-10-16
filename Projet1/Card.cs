using System;
using System.Collections.Generic;
using System.IO;
namespace Projet1
{
    public class Card
    {
        private int id;
        private int size;
        private List<string> value = new List<string>();

        public Card(int id, int size, string name)
        {
            this.id = id;
            this.size = size;
            string path = Directory.GetCurrentDirectory() + "\\card\\";
            try
            {
                Console.WriteLine(path);
                StreamReader sr = new StreamReader(path+name+".txt");

                string line = "";
                while (line != null)
                {
                    line = sr.ReadLine();
                    value.Add(line);
                }

                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void Draw()
        {
            Console.SetCursorPosition(0,0);
            for (int i = 0; i < this.value.Count; i++)
            {
                for (int j = 0; j < this.value[i].Length; j++)
                {
                    Console.Write(this.value[i][j]);
                }
                Console.WriteLine();
            }
        }
    }
}