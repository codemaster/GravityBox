using System;
using UnityEngine;
using Zenject;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private SoundConfig _soundConfig;

    [Inject]
    public void Initialize(ScoreTracker scoreTracker)
    {
        if (null == scoreTracker)
        {
            throw new ArgumentNullException(nameof(scoreTracker));
        }

        scoreTracker.OnScoreIncreased += OnScoreIncreased;
    }

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

    // TODO: BGM?
}
