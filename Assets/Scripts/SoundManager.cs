using System;
using UnityEngine;

public class SoundManager
{
    public SoundManager(ScoreTracker scoreTracker)
    {
        if (null == scoreTracker)
        {
            throw new ArgumentNullException(nameof(scoreTracker));
        }

        scoreTracker.OnScoreIncreased += OnScoreIncreased;
    }

    private void OnScoreIncreased(int score)
    {
        // TODO: Modulo the score by the number of hit sounds we have
        // TODO: Play that sound!
    }

    // TODO: BGM?
}
