/// <summary>
/// Text shown after finishing a level
/// </summary>
public class LevelOutroText : LevelText
{
    /// <summary>
    /// The died text
    /// </summary>
    private const string DiedString = "You died";

    /// <summary>
    /// The success text
    /// </summary>
    private const string FinishStringFormat = "You finished level {0}";

    /// <summary>
    /// Sets the text to be that the player died
    /// </summary>
    public void SetDied()
    {
        SetText(DiedString);
    }

    /// <summary>
    /// Sets the finished level text
    /// </summary>
    /// <param name="levelNumber">Level number.</param>
    public void SetFinishedLevel(int levelNumber)
    {
        SetText(string.Format(FinishStringFormat, levelNumber));
    }
}
