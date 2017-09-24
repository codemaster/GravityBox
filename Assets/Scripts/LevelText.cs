using TMPro;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Text used within a level that can optionally fade
/// </summary>
[RequireComponent(typeof(TextMeshProUGUI))]
public class LevelText : MonoBehaviour, IFadeable
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
	/// The text component.
	/// </summary>
	private TextMeshProUGUI _textComponent;

	/// <summary>
	/// When the object is initially created
	/// </summary>
	private void Awake()
	{
		_textComponent = GetComponent<TextMeshProUGUI>();
		if (null != _textComponent && _textComponent.enabled)
		{
			_textComponent.alpha = 0f;
		}
	}

	/// <summary>
	/// Fades in the text
	/// </summary>
	public void FadeIn()
	{
		StartCoroutine(FadeHelper.FadeOverTime(_textComponent, 1f, FadeInTime, OnFadeIn));
	}

	/// <summary>
	/// Fades out the text
	/// </summary>
	public void FadeOut()
	{
		StartCoroutine(FadeHelper.FadeOverTime(_textComponent, 0f, FadeOutTime, OnFadeOut));
	}

    /// <summary>
    /// Sets the text
    /// </summary>
    /// <param name="text">Text.</param>
    protected void SetText(string text)
    {
        if (null != _textComponent)
        {
            _textComponent.text = text;
        }
    }
}
