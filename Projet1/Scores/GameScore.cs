using System.Collections.Generic;

namespace Projet1;

public class GameScore
{
    private List<PlayerScore> _playerScores;
    private PlayerScore _bestScore;
    private int _gameId;
    public GameScore(int gameId)
    {
        _gameId = gameId;
        _playerScores = new List<PlayerScore>();
    }
    
    public GameScore(int gameId, Player bestPlayer, int bestScore)
    {
        _playerScores = new List<PlayerScore>();
        _gameId = gameId;
        _bestScore = new PlayerScore(bestPlayer, bestScore);
    }
    public int GameId
    {
        get => _gameId;
        set => _gameId = value;
    }
    
    
    public void AddScore(Player player, int score)
    {
        bool done = false;
        for (int i = 0; i < _playerScores.Count; i++)
        {
            if (_playerScores[i].Player == player)
            {
                _playerScores[i].Score += score;
                done = true;
            }
        }

        if (!done)
        {
            _playerScores.Add(new PlayerScore(player, score));
        }
        
        if (_bestScore != null)
        {
            for (int i = 0; i < _playerScores.Count; i++)
            {
                if (_playerScores[i].Score > _bestScore.Score)
                {
                    _bestScore = _playerScores[i];
                }
            }
        }
        else
        {
            _bestScore = new PlayerScore(player, score);
        }
        
    }

    public string SaveBestScore()
    {
        return _gameId + "|" + _bestScore;
    }

    public void EndScore()
    {
        _playerScores.Clear();
    }

    public int GetScore(Player p)
    {
        for(int i = 0; i < _playerScores.Count; i++)
        {
            if (_playerScores[i].Player == p)
            {
                return _playerScores[i].Score;
            }
        }

        return 0;
    }

    public string GetBestScore()
    {
        return _bestScore.ToString();
    }

    
    
    
    
}