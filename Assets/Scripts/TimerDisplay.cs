using System.Diagnostics;
using TMPro;
using UnityEngine;
using Zenject;

/// <summary>
/// Timer display.
/// </summary>
[RequireComponent(typeof(TextMeshProUGUI))]
public class TimerDisplay : MonoBehaviour
{
    /// <summary>
    /// The text component.
    /// </summary>
    private TextMeshProUGUI _textComponent;

    /// <summary>
    /// The stopwatch we are monitoring
    /// </summary>
    private Stopwatch _stopwatch;

    /// <summary>
    /// Initialization
    /// </summary>
    private void Awake()
    {
        _textComponent = GetComponent<TextMeshProUGUI>();
        _textComponent.enabled = false;
    }

    /// <summary>
    /// Sets the timer for the display
    /// </summary>
    /// <param name="stopwatch">Stopwatch.</param>
    public void SetTimer(Stopwatch stopwatch)
    {
        if (null == stopwatch)
        {
            Stop();
        } else {
            _stopwatch = stopwatch;
            _textComponent.enabled = true;
        }
    }

    /// <summary>
    /// Stops the display
    /// </summary>
    public void Stop()
    {
        _stopwatch = null;
        _textComponent.enabled = false;
    }

    /// <summary>
    /// Late update to update & display the timer's current value
    /// </summary>
    private void LateUpdate()
    {
        if (null == _textComponent || null == _stopwatch)
        {
            return;
        }

        _textComponent.text = _stopwatch.Elapsed.ToString("hh\\:mm\\:ss");
    }
}
