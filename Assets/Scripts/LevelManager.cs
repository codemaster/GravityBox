using System;

/// <summary>
/// Manages each level (scene)
/// </summary>
public class LevelManager
{
    /// <summary>
    /// The level loader.
    /// </summary>
    private LevelLoader _levelLoader;
    
	/// <summary>
    /// The level camera.
    /// </summary>
    private LevelCamera _levelCamera;

    /// <summary>
    /// The sound manager.
    /// </summary>
    private SoundManager _soundManager;
    
    /// <summary>
    /// The gravity controller.
    /// </summary>
    private GravityController _gravityController;

    /// <summary>
    /// The time tracker.
    /// </summary>
    private TimeTracker _timeTracker;
    
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
    /// <param name="levelLoader">Level loader.</param>
	/// <param name="levelCamera">Level camera.</param>
	/// <param name="gravityController">Gravity controller.</param>
    /// <param name="timeTracker">Time Tracker.</param>
	/// <param name="scoreTracker">Score tracker.</param>
	/// <param name="levelIntroText">Level intro text.</param>
    /// <param name="endLevelButton">End level button.</param>
	public LevelManager(
        LevelLoader levelLoader,
        LevelCamera levelCamera,
        GravityController gravityController,
        SoundManager soundManager,
        ScoreTracker scoreTracker,
        TimeTracker timeTracker,
        LevelIntroText levelIntroText,
        LevelOutroText levelOutroText,
        EndLevelButton endLevelButton)
    {
        if (null == levelLoader)
        {
            throw new ArgumentNullException(nameof(levelLoader));
        }
        if (null == levelCamera)
        {
            throw new ArgumentNullException(nameof(levelCamera));
        }
        if (null == gravityController)
        {
            throw new ArgumentNullException(nameof(gravityController));
        }
        if (null == soundManager)
        {
            throw new ArgumentNullException(nameof(soundManager));
        }
        if (null == timeTracker)
        {
            throw new ArgumentNullException(nameof(timeTracker));
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

        _levelLoader = levelLoader;
        _levelCamera = levelCamera;
        _gravityController = gravityController;
        _timeTracker = timeTracker;
        _soundManager = soundManager;
        _scoreTracker = scoreTracker;
        _levelIntroText = levelIntroText;
        _levelOutroText = levelOutroText;
        _endLevelButton = endLevelButton;

        // Setup callbacks
        _scoreTracker.OnScoreIncreased += OnScoreIncreased;
        _levelIntroText.OnFadeIn.AddListener(OnLevelIntroTextFadedIn);
        _levelIntroText.OnFadeOut.AddListener(OnLevelIntroTextFadedOut);

		// Update the intro text
		_levelIntroText.SetLevel(_levelLoader.LevelNumber);

		// Start showing the text
		_levelIntroText.FadeIn();

		// Fade out outro items
		_levelOutroText.FadeOut();
		_endLevelButton.FadeOut();

		// Reset & recalculate the target score when the scene has loaded
		_scoreTracker.Initialize();
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

        // Start the music
        _soundManager.StartBGM(_levelLoader.LevelNumber);

        // Start the timer
        _timeTracker.StartTimer(_levelLoader.LevelNumber);
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

        // Stop the timer
        _timeTracker.StopTimer(_levelLoader.LevelNumber);

        // Stop the music
        _soundManager.StopBGM();

        // Set the ending text to the current level
        _levelOutroText.SetFinishedLevel(_levelLoader.LevelNumber);

        // Turn off the gravity controller
        _gravityController.Enabled = false;

        // Show outro text & button
        _levelOutroText.FadeIn();
        _endLevelButton.FadeIn();
    }
}
