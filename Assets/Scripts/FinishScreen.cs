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
            // TODO: Calculate time
            _congratsText.text = string.Format(CongratsMessageFormat, "00:00:00");
        }
	}

    /// <summary>
    /// When the retry button is clicked, restart the game
    /// </summary>
    private void OnRetryClick()
    {
        StartCoroutine(_levelLoader.Restart());
    }
}
