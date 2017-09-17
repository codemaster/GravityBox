using System;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages each level (scene)
/// </summary>
public class LevelManager
{
    /// <summary>
    /// The level camera.
    /// </summary>
    private LevelCamera _levelCamera;
    
    /// <summary>
    /// The gravity controller.
    /// </summary>
    private GravityController _gravityController;
    
    /// <summary>
    /// The score tracker.
    /// </summary>
    private ScoreTracker _scoreTracker;

    /// <summary>
    /// Text used to introduce a level
    /// </summary>
    private LevelIntroText _levelText;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="levelCamera">Level camera.</param>
    /// <param name="gravityController">Gravity controller.</param>
    /// <param name="scoreTracker">Score tracker.</param>
    /// <param name="levelText">Level text</param>
    public LevelManager(
        LevelCamera levelCamera,
        GravityController gravityController,
        ScoreTracker scoreTracker,
        LevelIntroText levelText)
    {
        if (null == levelCamera)
        {
            throw new ArgumentNullException(nameof(levelCamera));
        }
        if (null == gravityController)
        {
            throw new ArgumentNullException(nameof(gravityController));
        }
        if (null == scoreTracker)
        {
            throw new ArgumentNullException(nameof(scoreTracker));
        }
        if (null == levelText)
        {
            throw new ArgumentNullException(nameof(levelText));
        }

        _levelCamera = levelCamera;
        _gravityController = gravityController;
        _scoreTracker = scoreTracker;
        _levelText = levelText;

        _scoreTracker.OnScoreIncreased += OnScoreIncreased;
        SceneManager.sceneLoaded += OnSceneLoaded;

        _levelText.OnFadeIn.AddListener(OnLevelTextFadedIn);
        _levelText.OnFadeOut.AddListener(OnLevelTextFadedOut);
	}

    /// <summary>
    /// Destructor
    /// </summary>
    ~LevelManager()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnLevelTextFadedIn()
    {
        _levelText.OnFadeIn.RemoveListener(OnLevelTextFadedIn);
        _levelText.FadeOut();
    }

    private void OnLevelTextFadedOut()
    {
        _levelText.OnFadeOut.RemoveListener(OnLevelTextFadedOut);

		// Then let the camera conduct its following logic
		_levelCamera.Enabled = true;

		// Enable the gravity controller, now that we've introduced the level
		_gravityController.Enabled = true;
    }

    /// <summary>
    /// Handler for when the scene has loaded
    /// </summary>
    /// <param name="scene">The scene that was loaded</param>
    /// <param name="mode">How the scene was loaded</param>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Start showing the text
        _levelText.FadeIn();

        // Reset & recalculate the target score when the scene has loaded
        _scoreTracker.Initialize();
    }

    /// <summary>
    /// Handler for when the player's score increases
    /// </summary>
    /// <param name="score">The new score amount</param>
    private void OnScoreIncreased(int score)
    {
        if (!_scoreTracker.Completed)
        {
            return;
        }

        // TODO: Show winning text
        // TODO: Then, transition to next level
    }
}
