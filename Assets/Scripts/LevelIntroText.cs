using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Text used to introduce a level
/// </summary>
[RequireComponent(typeof(TextMeshProUGUI))]
public class LevelIntroText : MonoBehaviour
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
        StartCoroutine(FadeOverTime(_textComponent, 1f, FadeInTime, OnFadeIn));
    }

    /// <summary>
    /// Fades out the text
    /// </summary>
    public void FadeOut()
    {
        StartCoroutine(FadeOverTime(_textComponent, 0f, FadeOutTime, OnFadeOut));
    }

    /// <summary>
    /// Fades the text to an alpha value over an amount of time
    /// </summary>
    /// <returns>Enumerator for coroutine.</returns>
    /// <param name="text">Text.</param>
    /// <param name="alphaValue">Alpha value.</param>
    /// <param name="timeToFade">Time to fade.</param>
    /// <param name="callback">Callback event to trigger.</param>
    private IEnumerator FadeOverTime(
        TextMeshProUGUI text,
        float alphaValue,
        float timeToFade,
        UnityEvent callback = null)
    {
        // Validate
        if (null == text || Mathf.Abs(text.alpha - alphaValue) < Mathf.Epsilon)
        {
            yield break;
        }

        // Fade
        var fadeAmount = 0f;
        while (Mathf.Abs(alphaValue - text.alpha) > Mathf.Epsilon)
        {
            fadeAmount += Time.deltaTime / timeToFade;
            text.alpha = Mathf.Lerp(text.alpha, alphaValue, fadeAmount);
            yield return null;
        }

        // Trigger callback if defined
        callback?.Invoke();
    }
}
