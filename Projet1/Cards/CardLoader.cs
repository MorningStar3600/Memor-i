using System;
using System.Collections.Generic;
using System.IO;

namespace Projet1
{
    class CardLoader
    {
        public static int Load(string directory, List<List<ColoredChar[]>> cards, ProgressBar pb = null)
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory())?.FullName + "\\cards";
            int width = 0;
            if (Directory.Exists(path + "\\" + directory))
            {
                
                path = path + "\\" + directory;
                string[] files = Directory.GetFiles(path);
                
                for(int i = 0; i < files.Length; i++)
                {
                    
                    List<ColoredChar[]> card = new List<ColoredChar[]>();
                    int width2 = ReadCardFile(files[i], card);
                    if (width2 > width) width = width2;
                    cards.Add(card);
                    if (pb != null && i%2 == 0)
                    {
                        pb.Update();
                    }
                }
            }
            
            return width;
        }
        private static int ReadCardFile(string path, List<ColoredChar[]> cardValue)
        {
            string line = "";
            int maxWidth = 0;
            try
            {
                StreamReader sr = new StreamReader(path);

                
                while (line != null)
                {
                    line = sr.ReadLine();
                    if (line == null) break;
                    
                    string[] coloredPixels = line.Split('');
                    List<ColoredChar> rslt = new List<ColoredChar>();
                    for (int j = 1; j < coloredPixels.Length-1; j++)
                    {
                        string pixels = coloredPixels[j].Split('m')[1];

                        string[] colors = coloredPixels[j].Split('m')[0].Split('[')[1].Split(';');
                        string value1;
                        string value2;
                        if (colors.Length == 4)
                        {
                            value1 = colors[1];
                            value2 = colors[3];
                        }
                        else
                        {
                            value1 = colors[0];
                            value2 = colors[2];
                        }
                        for (int k = 0; k < pixels.Length; k++)
                        {
                            rslt.Add(new ColoredChar(pixels[k], ColoredChar.GetColor(colors[0], colors[2]), ColoredChar.GetColor(value1, value2)));
                        }
                    }
                    if (line.Length > maxWidth) maxWidth = line.Length;
                    ColoredChar[] arr = new ColoredChar[rslt.Count];
                    for (int k = 0; k < rslt.Count; k++)
                    {
                        arr[k] = rslt[k];
                    }
                    cardValue.Add(arr);
                    rslt.Clear();
                }
                sr.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }

            return maxWidth;
        }
    }
}