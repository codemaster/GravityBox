using System.Collections.Generic;
using System.Diagnostics;

/// <summary>
/// Keeps track of level timers
/// </summary>
public class TimeTracker
{
    /// <summary>
    /// The timers per level index
    /// </summary>
    private readonly Dictionary<int, Stopwatch> _timers = new Dictionary<int, Stopwatch>();

    /// <summary>
    /// Starts a timer for a level index
    /// </summary>
    /// <param name="level">Level.</param>
    public void StartTimer(int level)
    {
        _timers[level] = new Stopwatch();
        _timers[level].Start();
    }

    /// <summary>
    /// Stops a timer for a level index
    /// </summary>
    /// <param name="level">Level.</param>
    public void StopTimer(int level)
    {
        Stopwatch timer = null;
        if(_timers.TryGetValue(level, out timer))
        {
            timer.Stop();
        }
    }

    /// <summary>
    /// Resets and clears all of the timers
    /// </summary>
    public void Reset()
    {
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
