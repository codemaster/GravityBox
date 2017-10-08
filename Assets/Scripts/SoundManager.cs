using System;
using UnityEngine;
using Zenject;

/// <summary>
/// Sound manager.
/// </summary>
public class SoundManager : MonoBehaviour
{
    /// <summary>
    /// The sound config.
    /// </summary>
    [SerializeField]
    private SoundConfig _soundConfig;

    /// <summary>
    /// The audio component.
    /// </summary>
    private AudioSource _audioComponent;

    /// <summary>
    /// Initialize the specified scoreTracker.
    /// </summary>
    /// <returns>The initialize.</returns>
    /// <param name="scoreTracker">Score tracker.</param>
    [Inject]
    public void Initialize(ScoreTracker scoreTracker)
    {
        if (null == scoreTracker)
        {
            throw new ArgumentNullException(nameof(scoreTracker));
        }

        _audioComponent = gameObject.AddComponent<AudioSource>();
        scoreTracker.OnScoreIncreased += OnScoreIncreased;
    }

    /// <summary>
    /// When the score has increased
    /// </summary>
    /// <param name="score">Score.</param>
    private void OnScoreIncreased(int score)
    {
        var pitch = 1.0f;
        if (_soundConfig.ScorePitches.Length > 0)
        {
            var pitchNum = (score - 1) % _soundConfig.ScorePitches.Length;
            pitch = _soundConfig.ScorePitches[pitchNum];
        }

        OneShot(_soundConfig.ScoreSound, pitch);
    }

    /// <summary>
    /// Plays an audio clip once
    /// </summary>
    /// <param name="clip">Clip.</param>
    /// <param name="pitch">Pitch.</param>
    /// <param name="volume">Volume.</param>
    private static void OneShot(AudioClip clip, float pitch = 1.0f, float volume = 1.0f)
    {
        if (null == clip)
        {
            throw new ArgumentNullException(nameof(clip));
        }
        
        var obj = new GameObject();
        var audio = obj.AddComponent<AudioSource>();
        audio.pitch = pitch;
        audio.PlayOneShot(clip, volume);
        Destroy(obj, clip.length / pitch);
    }

    /// <summary>
    /// Starts the background music
    /// </summary>
    /// <param name="levelNumber">Level number.</param>
    public void StartBGM(int levelNumber)
    {
        if(_audioComponent == null)
        {
            return;
        }

        StopBGM();

        _audioComponent.pitch = DetermineBGMPitchByLevel(levelNumber);
        _audioComponent.clip = _soundConfig.BGM;
        _audioComponent.loop = true;
        _audioComponent.Play();
    }

    /// <summary>
    /// Stops the background music
    /// </summary>
    public void StopBGM()
    {
        _audioComponent.Stop();
    }

    /// <summary>
    /// Pauses the bgm.
    /// </summary>
    public void PauseBGM()
    {
        _audioComponent.Pause();
    }

    /// <summary>
    /// Unpauses the bgm.
    /// </summary>
    public void UnpauseBGM()
    {
        _audioComponent.UnPause();
    }

    /// <summary>
    /// Determines the background music's pitch amount by the current level
    /// </summary>
    /// <returns>The BGMP itch by level.</returns>
    /// <param name="levelNumber">Level number.</param>
    private float DetermineBGMPitchByLevel(int levelNumber)
    {
        var pitchMod = levelNumber * (float)Math.Pow(-1, levelNumber) * 0.05f;
        return 1.0f + pitchMod;
    }
}
