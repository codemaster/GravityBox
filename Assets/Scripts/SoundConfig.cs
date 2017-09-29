using UnityEngine;

/// <summary>
/// Sound config.
/// </summary>
[CreateAssetMenu]
public class SoundConfig : ScriptableObject
{
    /// <summary>
    /// Background music
    /// </summary>
    public AudioClip BGM;

    /// <summary>
    /// Score sounds
    /// </summary>
    public AudioClip ScoreSound;

    /// <summary>
    /// Score pitches
    /// </summary>
    public float[] ScorePitches;
}
