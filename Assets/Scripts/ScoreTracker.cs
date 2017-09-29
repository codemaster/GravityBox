using UnityEngine;

/// <summary>
/// Score tracker.
/// </summary>
public class ScoreTracker
{
    /// <summary>
    /// Delegate for when the score has increased
    /// </summary>
    public delegate void ScoreIncreased(int updatedScore);

    /// <summary>
    /// Event that occurs when the score has increased.
    /// </summary>
    public event ScoreIncreased OnScoreIncreased;

    /// <summary>
    /// The current score
    /// </summary>
    private int _score;

    /// <summary>
    /// The score we are trying to achieve
    /// </summary>
    private int _targetScore;

    /// <summary>
    /// The score getter/setter
    /// </summary>
    /// <value>The score.</value>
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

    /// <summary>
    /// If the level is completed or not
    /// </summary>
    /// <value><c>true</c> if completed; otherwise, <c>false</c>.</value>
    public bool Completed
    {
        get
        {
            return _score >= _targetScore;
        }
    }

    /// <summary>
    /// Initializes the score tracker
    /// </summary>
    public void Initialize()
    {
        // Reset the score
        _score = 0;
        // Calculate our target score
        _targetScore = Object.FindObjectsOfType<WallCollider>().Length;
    }
}
