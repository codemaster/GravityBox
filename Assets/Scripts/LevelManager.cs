using System;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages each level (scene)
/// </summary>
public class LevelManager
{
    /// <summary>
    /// The score tracker.
    /// </summary>
    private ScoreTracker _scoreTracker;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="scoreTracker">Score tracker.</param>
    public LevelManager(ScoreTracker scoreTracker)
    {
        if (null == scoreTracker)
        {
            throw new ArgumentNullException(nameof(scoreTracker));
        }

        _scoreTracker = scoreTracker;

        _scoreTracker.OnScoreIncreased += OnScoreIncreased;
        SceneManager.sceneLoaded += OnSceneLoaded;
	}

    /// <summary>
    /// Destructor
    /// </summary>
    ~LevelManager()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /// <summary>
    /// Handler for when the scene has loaded
    /// </summary>
    /// <param name="scene">The scene that was loaded</param>
    /// <param name="mode">How the scene was loaded</param>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reset & recalculate the target score when the scene has loaded
        _scoreTracker.Initialize();
    }

    /// <summary>
    /// Handler for when the player's score increases
    /// </summary>
    /// <param name="score">The new score amount</param>
    private void OnScoreIncreased(int score)
    {
        UnityEngine.Debug.Log("Score is now " + score);
        if (!_scoreTracker.Completed)
        {
            return;
        }

        // TODO: Show winning text
        // TODO: Then, transition to next level
    }
}
