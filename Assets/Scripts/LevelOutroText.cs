using Zenject;

/// <summary>
/// Text shown after finishing a level
/// </summary>
public class LevelOutroText : LevelText
{
    /// <summary>
    /// The time tracker.
    /// </summary>
    [Inject]
    private TimeTracker _timeTracker;

    /// <summary>
    /// The level loader.
    /// </summary>
    [Inject]
    private LevelLoader _levelLoader;
    
    /// <summary>
    /// The success text
    /// </summary>
    private const string FinishStringFormat = "You finished level {0}\n{1}";

    /// <summary>
    /// Sets the finished level text
    /// </summary>
    /// <param name="levelNumber">Level number.</param>
    public void SetFinishedLevel(int levelNumber)
    {
        var timer = _timeTracker.GetTimer(_levelLoader.LevelNumber);
        SetText(string.Format(FinishStringFormat, levelNumber, timer.Elapsed.ToString("hh\\:mm\\:ss")));
    }
}
