using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Zenject;

/// <summary>
/// Keeps track of level timers
/// </summary>
public class TimeTracker : MonoBehaviour
{
    /// <summary>
    /// The time display
    /// </summary>
    private TimerDisplay _display;

    /// <summary>
    /// The level loader.
    /// </summary>
    private LevelLoader _levelLoader;
    
    /// <summary>
    /// The timers per level index
    /// </summary>
    private readonly Dictionary<int, Stopwatch> _timers
        = new Dictionary<int, Stopwatch>();

    /// <summary>
    /// Initializer
    /// </summary>
    /// <param name="display">Display.</param>
    [Inject]
    public void Initialize(
        TimerDisplay display,
        LevelLoader levelLoader)
    {
        if (null == display)
        {
            throw new ArgumentNullException(nameof(display));
        }
        if (null == levelLoader)
        {
            throw new ArgumentNullException(nameof(levelLoader));
        }

        _display = display;
        _levelLoader = levelLoader;
    }

    /// <summary>
    /// Gets the timer.
    /// </summary>
    /// <returns>The timer.</returns>
    /// <param name="level">Level.</param>
    public Stopwatch GetTimer(int level)
    {
		Stopwatch timer = null;
        _timers.TryGetValue(level, out timer);
        return timer;
    }

    /// <summary>
    /// Starts a timer for a level index
    /// </summary>
    /// <param name="level">Level.</param>
    public void StartTimer(int level)
    {
        var timer = new Stopwatch();
        _timers[level] = timer;
        timer.Start();

        if (null != _display)
        {
            _display.SetTimer(timer);
        }
    }

    /// <summary>
    /// Stops a timer for a level index
    /// </summary>
    /// <param name="level">Level.</param>
    public void StopTimer(int level)
    {
        Stopwatch timer = GetTimer(level);
        if(timer != null)
        {
            timer.Stop();
        }

		if (null != _display)
		{
			_display.Stop();
		}
    }

    /// <summary>
    /// Resets and clears all of the timers
    /// </summary>
    public void Reset()
    {
		if (null != _display)
		{
			_display.Stop();
		}

        foreach (var timer in _timers.Values)
        {
            timer.Stop();
        }
        _timers.Clear();
    }

    /// <summary>
    /// Obtains the number of milliseconds all of the tiemrs took
    /// </summary>
    /// <returns>The milliseconds.</returns>
    public long TotalMilliseconds()
    {
        long milliseconds = 0;
        foreach (var timer in _timers.Values)
        {
            milliseconds += timer.ElapsedMilliseconds;
        }
        return milliseconds;
    }

    /// <summary>
    /// Pauses the time tracker
    /// </summary>
    /// <param name="level">Level.</param>
    public void Pause(int level)
    {
        Stopwatch timer = GetTimer(level);
        if(timer != null)
        {
            timer.Stop();
        }
    }

    /// <summary>
    /// Resumes the time tracker
    /// </summary>
    /// <param name="level">Level.</param>
    public void Resume(int level)
    {
        Stopwatch timer = GetTimer(level);
        if(timer != null)
        {
            timer.Start();
        }
    }

	/// <summary>
	/// On the application focus change.
	/// </summary>
	/// <param name="hasFocus">If set to <c>true</c>, app has focus.</param>
	private void OnApplicationFocus(bool hasFocus)
	{
		if (hasFocus)
		{
			Resume(_levelLoader.LevelNumber);
		}
		else
		{
			Pause(_levelLoader.LevelNumber);
		}
	}

	/// <summary>
	/// On the application pause change.
	/// </summary>
	/// <param name="pauseStatus">If set to <c>true</c>, the app is paused.</param>
	private void OnApplicationPause(bool pauseStatus)
	{
		if (pauseStatus)
		{
			Pause(_levelLoader.LevelNumber);
		}
		else
		{
			Resume(_levelLoader.LevelNumber);
		}
	}
}
