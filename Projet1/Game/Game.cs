using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Projet1
{
    public class Game
    {
        private int _idGame;
        public List<Player> _players;
        private int _indexPlayer;
        public int maxScore;
        
        public Game(int id, string[] names, int maxScore = 1000)
        {
            Score.Load();
            _idGame = id;
            _indexPlayer = 0;
            _players = new List<Player>();
            this.maxScore = maxScore;
            for (int i = 0; i < names.Length; i++)
            {
                _players.Add(new Player(names[i])
                {
                    color = (ConsoleColor)i+1,
                    character = '0'
                });
            }
            
        }
        
        public Game(int id, List<Player> players, int maxScore = 1000)
        {
            _idGame = id;
            _indexPlayer = 0;
            _players = players;
            this.maxScore = maxScore;
        }
        //Pseudo|id jeu1\nbr victoires\nbr défaites\meilleur score|id jeu2\nbr victoires\nbr défaites\meilleur score
        public string GetBestScoreFromGame(int gameId)
        {
            return Score.GetHighScore(gameId);  
        }

        public Player GetCurrentPlayer()
        {
            return _players[_indexPlayer];
        }
        
        public Player NextPlayer()
        {
            _indexPlayer++;
            if (_indexPlayer >= GetNumbPlayers())
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
            Score.AddScore(_idGame, player, score);
        }
        
        public int GetScore(Player player)
        {
            return Score.GetScore(_idGame, player);
        }
        
        public int IdGame
        {
            get => _idGame;
            set => _idGame = value;
        }
        
        public int GetNumbPlayers()
        {
            return _players.Count;
        }

        public void EndGame()
        {
            Score.EndScores();
        }

        public void AddPlayer(string name, ConsoleColor color, char c)
        {
            var newPlayer = new Player(name)
            {
                color = color,
                character = c
            };
            _players.Add(newPlayer);
        }
        
        public void RemovePlayer()
        {
            _players.RemoveAt(GetNumbPlayers() - 1);
        }
        
    }
}