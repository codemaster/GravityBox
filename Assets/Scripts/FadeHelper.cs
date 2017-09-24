using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;
using TMPro;

/// <summary>
/// Helpers for fading things in & out
/// </summary>
public static class FadeHelper
{
    /// <summary>
    /// Fades text to an alpha value over an amount of time
    /// </summary>
    /// <returns>Enumerator for coroutine.</returns>
    /// <param name="text">The text.</param>
    /// <param name="alphaValue">Alpha value to fade to.</param>
    /// <param name="timeToFade">Time to fade.</param>
    /// <param name="callback">Callback event to trigger.</param>
    public static IEnumerator FadeOverTime(
        TMP_Text text,
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

	/// <summary>
    /// Fades a graphic's alpha to an alpha value over an amount of time
	/// </summary>
	/// <returns>Enumerator for coroutine.</returns>
	/// <param name="graphic">The graphic to fade.</param>
	/// <param name="alphaValue">Alpha value to fade to.</param>
	/// <param name="timeToFade">Time to fade.</param>
	/// <param name="callback">Callback event to trigger.</param>
	public static IEnumerator FadeOverTime(
        Graphic graphic,
		float alphaValue,
		float timeToFade,
		UnityEvent callback = null)
	{
        // Validate
        if (null == graphic || Mathf.Abs(graphic.color.a - alphaValue) < Mathf.Epsilon)
		{
			yield break;
		}

		// Fade
		var fadeAmount = 0f;
        while (Mathf.Abs(alphaValue - graphic.color.a) > Mathf.Epsilon)
		{
			fadeAmount += Time.deltaTime / timeToFade;
            var newAlpha = Mathf.Lerp(graphic.color.a, alphaValue, fadeAmount);
            var newColor = new Color(graphic.color.r, graphic.color.g, graphic.color.b, newAlpha);
            graphic.color = newColor;
			yield return null;
		}

		// Trigger callback if defined
		callback?.Invoke();
	}
}
