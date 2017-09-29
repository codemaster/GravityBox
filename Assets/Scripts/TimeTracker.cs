using System;
using System.Collections.Generic;
using System.Diagnostics;

/// <summary>
/// Keeps track of level timers
/// </summary>
public class TimeTracker
{
    private TimerDisplay _display;
    
    /// <summary>
    /// The timers per level index
    /// </summary>
    private readonly Dictionary<int, Stopwatch> _timers
        = new Dictionary<int, Stopwatch>();

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="display">Display.</param>
    public TimeTracker(TimerDisplay display)
    {
        if (null == display)
        {
            throw new ArgumentNullException(nameof(display));
        }

        _display = display;
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
}
