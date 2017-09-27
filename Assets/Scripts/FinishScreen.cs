using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

/// <summary>
/// Finish screen.
/// </summary>
public class FinishScreen : MonoBehaviour
{
    /// <summary>
    /// The congrats message format.
    /// </summary>
    private const string CongratsMessageFormat = "Congratulations!\nYour time was {0}";

    /// <summary>
    /// The congrats text.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _congratsText;
    
    /// <summary>
    /// The retry button.
    /// </summary>
    [SerializeField]
    private Button _retryButton;
    
    /// <summary>
    /// The level loader.
    /// </summary>
    [Inject]
    LevelLoader _levelLoader;

    /// <summary>
    /// The outro text.
    /// </summary>
    [Inject]
    LevelOutroText _outroText;

    /// <summary>
    /// The end level button.
    /// </summary>
    [Inject]
    EndLevelButton _endLevelButton;

    /// <summary>
    /// The time tracker.
    /// </summary>
    [Inject]
    TimeTracker _timeTracker;
    
    /// <summary>
    /// Initialization
    /// </summary>
	private void Start()
	{
        if (null != _outroText)
        {
            _outroText.FadeOut();
        }

        if (null != _endLevelButton)
        {
            _endLevelButton.FadeOut();
        }

        if (null != _retryButton)
        {
            _retryButton.onClick.AddListener(OnRetryClick);
        }

        if (null != _congratsText)
        {
            _congratsText.text = string.Format(CongratsMessageFormat, GetCompletionTime());
        }
	}

    /// <summary>
    /// When the retry button is clicked, restart the game
    /// </summary>
    private void OnRetryClick()
    {
        StartCoroutine(_levelLoader.Restart());
    }

    /// <summary>
    /// Get the string representing the amount of time it took to complete the levels
    /// </summary>
    /// <returns>The completion time.</returns>
    private string GetCompletionTime()
    {
        if (_timeTracker == null)
        {
            return "Unknown Time";
        }

        var totalMs = _timeTracker.TotalMilliseconds();

        var numHours = (int)(totalMs / 3600000);
        totalMs -= (numHours * 3600000);

        var numMinutes = (int)(totalMs / 60000);
        totalMs -= (numMinutes * 60000);

        var numSeconds = (int)(totalMs / 1000);
        //totalMs -= (numSeconds * 1000);

        _timeTracker.Reset();

        return $"{numHours:00}:{numMinutes:00}:{numSeconds:00}";
    }
}
