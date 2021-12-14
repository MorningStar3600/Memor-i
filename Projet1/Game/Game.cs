using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Projet1
{
    public class Game
    {
        private int _idGame;
        public readonly Player[] _players;
        private int _indexPlayer;
        public int maxScore;
        
        public Game(int id, string[] names, int maxScore = 1000)
        {
            _idGame = id;
            _indexPlayer = 0;
            _players = new Player[names.Length];
            this.maxScore = maxScore;
            for (int i = 0; i < names.Length; i++)
            {
                _players[i] = new Player(names[i]);
            }

            _players[0].color = ConsoleColor.DarkGreen;
            _players[0].character = 'E';
            _players[1].color = ConsoleColor.Blue;
            _players[1].character = 'A';
            
        }

        public void SavePlayersToFile()
        {
            var path = Directory.GetParent(Directory.GetCurrentDirectory())?.FullName + "\\save\\save.txt";
            var lines = File.ReadAllLines(path);

            var linesValue = lines.ToList();
            foreach (var t in _players)
            {
                t.SavePlayer(linesValue);
            }
            
            var sw = new StreamWriter(path);
            foreach (var line in linesValue)
            {
                sw.WriteLine(line);
            }
            sw.Close();
        }
        
        public Player GetCurrentPlayer()
        {
            return _players[_indexPlayer];
        }
        
        public Player NextPlayer()
        {
            _indexPlayer++;
            if (_indexPlayer >= _players.Length)
            {
                _indexPlayer = 0;
            }
            return _players[_indexPlayer];
        }
        
        public void Win(Player player)
        {
            player.Win(_idGame);
        }
        
        public void Lose(Player player)
        {
            player.Lose(_idGame);
        }
        
        public void AddScore(Player player, int score)
        {
            player.AddScore(score);
        }
        
        public int GetScore(Player player)
        {
            return player.GetScore();
        }
        
        public void SetIdGame(int id)
        {
            _idGame = id;
        }
        
        public int GetNumbPlayers()
        {
            return _players.Length;
        }

        public void EndGame()
        {
            for (int i = 0; i < _players.Length; i++)
            {
                _players[i].actualScore = 0;
            }
        }
        
    }
}