using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
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
    /// The event system.
    /// </summary>
    [SerializeField]
    private EventSystem _eventSystem;

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
    /// The level loader.
    /// </summary>
    [Inject]
    private LevelLoader _levelLoader;

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

        OnFadeIn.AddListener(EnableComponents);
        OnFadeOut.AddListener(DisableComponents);

        _buttonComponent = GetComponent<Button>();
        _buttonComponent.onClick.AddListener(OnClick);
        DisableComponents();
    }
    
    /// <summary>
	/// Fades in the button
	/// </summary>
	public void FadeIn()
	{
		_text.enabled = true;
		_image.enabled = true;
		StartCoroutine(FadeHelper.FadeOverTime(_text, 1f, FadeInTime, OnFadeIn));
        StartCoroutine(FadeHelper.FadeOverTime(_image, 1f, FadeInTime, OnFadeIn));
        _buttonComponent.enabled = true;
	}

	/// <summary>
	/// Fades out the button
	/// </summary>
	public void FadeOut()
	{
		StartCoroutine(FadeHelper.FadeOverTime(_text, 0f, FadeOutTime, OnFadeOut));
        StartCoroutine(FadeHelper.FadeOverTime(_image, 0f, FadeOutTime, OnFadeOut));
        _buttonComponent.enabled = false;
		_text.enabled = false;
		_image.enabled = false;
	}

    /// <summary>
    /// Enables the components.
    /// </summary>
    private void EnableComponents()
    {
		_buttonComponent.enabled = true;
		_text.enabled = true;
		_image.enabled = true;
        _eventSystem.SetSelectedGameObject(gameObject);
    }

    /// <summary>
    /// Disables the components.
    /// </summary>
    private void DisableComponents()
    {
		_buttonComponent.enabled = false;
		_text.enabled = false;
		_image.enabled = false;
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
        StartCoroutine(_levelLoader.LoadNextLevel());
    }
}
