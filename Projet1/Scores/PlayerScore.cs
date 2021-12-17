using System.Collections.Generic;

namespace Projet1;

public class PlayerScore
{
    private Player _player;
    private int _score;
    
    
    public PlayerScore(Player p, int score)
    {
        _player = p;
        _score = score;
    }

    public Player Player
    {
        get => _player;
        set => _player = value;
    }

    public int Score
    {
        get => _score;
        set => _score = value;
    }


    public override string ToString()
    {
        string rslt = _player.GetName() + "\\"+_score;
        return rslt;
    }
}