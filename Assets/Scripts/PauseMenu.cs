using System;
using UnityEngine;
using Zenject;

/// <summary>
/// Pause menu.
/// </summary>
public sealed class PauseMenu : MonoBehaviour
{
    /// <summary>
    /// The parts of the pause menu
    /// </summary>
    [SerializeField]
    private GameObject _parts;

    /// <summary>
    /// If the game is paused or not
    /// </summary>
    private bool _paused;

    /// <summary>
    /// The time tracker.
    /// </summary>
    private TimeTracker _timeTracker;

    /// <summary>
    /// The level loader.
    /// </summary>
    private LevelLoader _levelLoader;

    /// <summary>
    /// The sound manager.
    /// </summary>
    private SoundManager _soundManager;

    /// <summary>
    /// When the resume button is clicked
    /// </summary>
    public void OnClickResume()
    {
        UnpauseGame();
    }

    /// <summary>
    /// When the exit button is clicked
    /// </summary>
    public void OnClickExit()
    {
        Application.Quit();
    }

    /// <summary>
    /// Initialize
    /// </summary>
    /// <param name="timeTracker">Time tracker.</param>
    [Inject]
    private void Initialize(
        TimeTracker timeTracker,
        LevelLoader levelLoader,
        SoundManager soundManager)
    {
        if (timeTracker == null)
        {
            throw new ArgumentNullException(nameof(timeTracker));
        }
        if (levelLoader == null)
        {
            throw new ArgumentNullException(nameof(levelLoader));
        }
        if (soundManager == null)
        {
            throw new ArgumentNullException(nameof(soundManager));
        }
        
        _timeTracker = timeTracker;
        _levelLoader = levelLoader;
        _soundManager = soundManager;
    }
    
    /// <summary>
    /// Self-initialization
    /// </summary>
    private void Awake()
    {
        UnpauseGame();
    }

    /// <summary>
    /// Handle each frame
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_paused)
            {
                UnpauseGame();
            } else {
                PauseGame();
            }
        }
	}

    /// <summary>
    /// Pauses the game.
    /// </summary>
    private void PauseGame()
    {
        _paused = true;
        Time.timeScale = 0;
        _parts.SetActive(true);
        _timeTracker.Pause(_levelLoader.LevelNumber);
        _soundManager.PauseBGM();
    }

    /// <summary>
    /// Unpauses the game.
    /// </summary>
    private void UnpauseGame()
    {
        _paused = false;
        Time.timeScale = 1;
        _parts.SetActive(false);
        _timeTracker.Resume(_levelLoader.LevelNumber);
        _soundManager.UnpauseBGM();
    }
}
