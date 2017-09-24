using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using Zenject;

/// <summary>
/// Button to show at the end of a level
/// </summary>
[RequireComponent(typeof(Button))]
public class EndLevelButton : MonoBehaviour, IFadeable
{
	/// <summary>
	/// The UnityEvent for when the text has faded in
	/// </summary>
	public UnityEvent OnFadeIn;

	/// <summary>
	/// The UnityEvent for when the text has faded out
	/// </summary>
	public UnityEvent OnFadeOut;

	/// <summary>
	/// The amount of time it will take the text to fade in
	/// </summary>
	public float FadeInTime;

	/// <summary>
	/// The amount of time it will take the text to fade out
	/// </summary>
	public float FadeOutTime;

    /// <summary>
    /// Text that will be on the button
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _text;

    /// <summary>
    /// Image that will be on the button
    /// </summary>
    [SerializeField]
    private Image _image;

    /// <summary>
    /// The level manager.
    /// </summary>
    [Inject]
    private LevelManager _levelManager;

    /// <summary>
    /// The button component.
    /// </summary>
    private Button _buttonComponent;

    /// <summary>
    /// Text to show on success
    /// </summary>
    private const string SuccessString = "Next Level";

    /// <summary>
    /// Text to show on failure
    /// </summary>
    private const string FailureString = "Retry Level";

    /// <summary>
    /// Initialize
    /// </summary>
    private void Awake()
    {
        // Make everything invisible initially
        if (null != _text)
        {
            _text.alpha = 0f;
        }

        if (null != _image)
        {
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0f);
        }

        _buttonComponent = GetComponent<Button>();
        _buttonComponent.onClick.AddListener(OnClick);
    }
    
    /// <summary>
	/// Fades in the button
	/// </summary>
	public void FadeIn()
	{
		StartCoroutine(FadeHelper.FadeOverTime(_text, 1f, FadeInTime, OnFadeIn));
        StartCoroutine(FadeHelper.FadeOverTime(_image, 1f, FadeInTime, OnFadeIn));
	}

	/// <summary>
	/// Fades out the button
	/// </summary>
	public void FadeOut()
	{
		StartCoroutine(FadeHelper.FadeOverTime(_text, 0f, FadeOutTime, OnFadeOut));
        StartCoroutine(FadeHelper.FadeOverTime(_image, 0f, FadeOutTime, OnFadeOut));
	}

    /// <summary>
    /// Sets the text of the button depending on success or failure
    /// </summary>
    /// <param name="success">If set to <c>true</c>, success, otherwise failure.</param>
    public void SetSuccees(bool success)
    {
        _text.text = success ? SuccessString : FailureString;
    }

    /// <summary>
    /// Handles clicking on the button
    /// </summary>
    private void OnClick()
    {
        _levelManager.LoadNextLevel();
    }
}
