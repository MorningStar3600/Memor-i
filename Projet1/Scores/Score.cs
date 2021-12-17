using System.Collections.Generic;
using System.IO;

namespace Projet1;

public static class Score
{
    private static List<GameScore> _gameScores = new List<GameScore>();
    
    public static void AddScore(int gameId, Player player, int score)
    {
        bool isDone = false;
        
        for (int i = 0; i < _gameScores.Count; i++)
        {
            if (_gameScores[i].GameId == gameId)
            {
                _gameScores[i].AddScore(player, score);
                isDone = true;
            }
        }

        if (!isDone)
        {
            GameScore gs = new GameScore(gameId);
            gs.AddScore(player, score);
            _gameScores.Add(gs);
        }
    }

    public static void Save()
    {
        string[] scores = new string[_gameScores.Count];
        for (int i = 0; i < _gameScores.Count; i++)
        {
            scores[i] = _gameScores[i].SaveBestScore();
        }

        string path = Directory.GetParent(Directory.GetCurrentDirectory()).FullName + "/save/save.txt";
        StreamWriter sr = new StreamWriter(path, false);
        
        for (int i = 0; i < scores.Length; i++)
        {
            sr.WriteLine(scores[i]);
        }
        sr.Close();
    }

    public static void Load()
    {
        string path = Directory.GetParent(Directory.GetCurrentDirectory()).FullName + "/save/save.txt";
        StreamReader sr = new StreamReader(path);

        string[] lines = File.ReadAllLines(path);
        
        for (int i = 0; i < lines.Length; i++)
        {
            string[] values = lines[i].Split('|');
            string[] bestPlayer = values[1].Split('\\');
            _gameScores.Add(new GameScore(int.Parse(values[0]), new Player(bestPlayer[0]), int.Parse(bestPlayer[1])));
        }
        sr.Close();
    }

    public static void EndScores()
    {
        for (int i = 0; i < _gameScores.Count; i++)
        {
            _gameScores[i].EndScore();
        }
    }

    public static int GetScore(int idGame, Player player)
    {
        for (int i = 0; i < _gameScores.Count; i++)
        {
            if (_gameScores[i].GameId == idGame)
            {
                return _gameScores[i].GetScore(player);
            }
        }

        return 0;
    }

    public static string GetHighScore(int idGame)
    {
        for (int i = 0; i < _gameScores.Count; i++)
        {
            if (_gameScores[i].GameId == idGame)
            {
                return _gameScores[i].GetBestScore();
            }
        }

        return ":";
    }
}