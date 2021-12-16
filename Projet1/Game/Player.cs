using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Projet1
{
    public class Player
    {
        private string name { get; set; }

        private List<int[]> scores { get; set; }

        public int actualScore { get; set; }

        public char character { get; set; }
        public ConsoleColor color { get; set; }

        //Pseudo|id jeu1\nbr victoires\nbr défaites\meilleur score|id jeu2\nbr victoires\nbr défaites\meilleur score

        public Player()
        {
            name = "";
            scores = new List<int[]>();
        }

        public Player(string n)
        {
            name = n;
            scores = new List<int[]>();
            GetPlayerFromFile();
        }

        public string GetName()
        {
            return name;
        }

        public void Win(int idGame)
        {
            var isSaved = false;
            foreach (var t in scores.Where(t => t[0] == idGame))
            {
                isSaved = true;
                t[1]++;
                if (t[3] < actualScore)
                {
                    t[3] = actualScore;
                }
            }

            if (!isSaved)
            {
                scores.Add(new int[] { idGame, 1, 0, actualScore });
            }
        }

        public void Lose(int idGame)
        {
            var isSaved = false;
            foreach (var t in scores.Where(t => t[0] == idGame))
            {
                isSaved = true;
                t[2]++;
            }

            if (!isSaved)
            {
                scores.Add(new int[] { idGame, 0, 1, actualScore });
            }
        }

        public void AddScore(int score)
        {
            actualScore += score;
        }

        public void GetPlayerFromFile()
        {
            scores.Clear();
            string path = Directory.GetParent(Directory.GetCurrentDirectory())?.FullName + "\\save\\save.txt";
            StreamReader sr = new StreamReader(path);

            string line = sr.ReadLine();
            while (line != null)
            {
                if (line.Split('|')[0] == name) break;
                line = sr.ReadLine();
            }

            sr.Close();

            if (line == null) return;
            var values = line.Split('|');

            for (var i = 1; i < values.Length; i++)
            {
                var v = values[i].Split('\\');
                scores.Add(new int[] { int.Parse(v[0]), int.Parse(v[1]), int.Parse(v[2]), int.Parse(v[3]) });
            }
        }

        /// <summary>
        /// Real function to change list of scores in file of player
        /// </summary>
        /// <param name="lines"></param>
        public void SavePlayer(List<string> lines, int id)
        {
            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].Split('|')[0] == name)
                {
                    string value = "";
                    for (int j = 0; j < scores.Count; j++)
                    {
                        if (j < scores.Count-1)
                        {
                            
                        }
                        else
                        {
                            
                        }
                    }
                    
                    return;
                }
            }

            lines.Add(scores.Aggregate(name,
                (current, t) => current + ("|" + t[0] + "\\" + t[1] + "\\" + t[2] + "\\" + t[3])));
        }

        public int GetScore()
        {
            return actualScore;
        }
    }
}