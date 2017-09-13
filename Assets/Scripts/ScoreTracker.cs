using UnityEngine;

public class ScoreTracker
{
    public delegate void ScoreIncreased(int updatedScore);
    public event ScoreIncreased OnScoreIncreased;

    private int _score;
    private int _targetScore;

    public int Score
    {
        get { return _score; }
        set
        {
            // Set the new score
            _score = value;
            // Trigger the updated event
            OnScoreIncreased?.Invoke(value);
        }
    }

    public bool Completed
    {
        get
        {
            return _score >= _targetScore;
        }
    }

    public void Initialize()
    {
        // Reset the score
        _score = 0;
        // Calculate our target score
        _targetScore = Object.FindObjectsOfType<WallCollider>().Length;
    }
}
