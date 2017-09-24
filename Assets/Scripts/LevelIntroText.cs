/// <summary>
/// Text used to introduce a level
/// </summary>
public class LevelIntroText : LevelText
{
    private const string StringFormat = "Level {0}";

    public void SetLevel(int levelNumber)
    {
        SetText(string.Format(StringFormat, levelNumber));
    }
}
