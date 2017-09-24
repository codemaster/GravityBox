using System;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages each level (scene)
/// </summary>
public class LevelManager
{
	/// <summary>
	/// What level number are we on
	/// </summary>
	public int LevelNumber { get; private set; } = 1;

    /// <summary>
    /// The next level to load
    /// </summary>
    private int _nextLevelToLoad;

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
    private LevelIntroText _levelIntroText;

    /// <summary>
    /// Text used for ending a level
    /// </summary>
    private LevelOutroText _levelOutroText;

    /// <summary>
    /// Button to show at the end of a level
    /// </summary>
    private EndLevelButton _endLevelButton;

	/// <summary>
	/// Constructor
	/// </summary>
	/// <param name="levelConfig">Level config.</param>
	/// <param name="levelCamera">Level camera.</param>
	/// <param name="gravityController">Gravity controller.</param>
	/// <param name="scoreTracker">Score tracker.</param>
	/// <param name="levelIntroText">Level intro text.</param>
    /// <param name="endLevelButton">End level button.</param>
	public LevelManager(
        LevelConfig levelConfig,
        LevelCamera levelCamera,
        GravityController gravityController,
        ScoreTracker scoreTracker,
        LevelIntroText levelIntroText,
        LevelOutroText levelOutroText,
        EndLevelButton endLevelButton)
    {
        if (null == levelConfig)
        {
            throw new ArgumentNullException(nameof(levelConfig));
        }
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
        if (null == levelIntroText)
        {
            throw new ArgumentNullException(nameof(levelIntroText));
        }
        if (null == levelOutroText)
        {
            throw new ArgumentNullException(nameof(levelOutroText));
        }
        if (null == endLevelButton)
        {
            throw new ArgumentNullException(nameof(endLevelButton));
        }

        _levelCamera = levelCamera;
        _gravityController = gravityController;
        _scoreTracker = scoreTracker;
        _levelIntroText = levelIntroText;
        _levelOutroText = levelOutroText;
        _endLevelButton = endLevelButton;

        _scoreTracker.OnScoreIncreased += OnScoreIncreased;
        SceneManager.sceneLoaded += OnSceneLoaded;

        _levelIntroText.OnFadeIn.AddListener(OnLevelIntroTextFadedIn);
        _levelIntroText.OnFadeOut.AddListener(OnLevelIntroTextFadedOut);
	}

    /// <summary>
    /// Destructor
    /// </summary>
    ~LevelManager()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /// <summary>
    /// Loads the next level.
    /// </summary>
    public void LoadNextLevel()
    {
        UnityEngine.Debug.Log("Load level #" + _nextLevelToLoad);
    }

    /// <summary>
    /// When the intro text has faded in
    /// </summary>
    private void OnLevelIntroTextFadedIn()
    {
        _levelIntroText.OnFadeIn.RemoveListener(OnLevelIntroTextFadedIn);
        _levelIntroText.FadeOut();
    }

    /// <summary>
    /// When the intro text has faded out
    /// </summary>
    private void OnLevelIntroTextFadedOut()
    {
        _levelIntroText.OnFadeOut.RemoveListener(OnLevelIntroTextFadedOut);

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
        _levelIntroText.FadeIn();

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

        // Turn off the gravity controller
        _gravityController.Enabled = false;

        // Show outro text & button
        _levelOutroText.FadeIn();
        _endLevelButton.FadeIn();

        // Set our next level to load to be the next level!
        _nextLevelToLoad = LevelNumber + 1;
    }
}
