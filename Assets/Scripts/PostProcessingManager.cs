using System;
using UnityEngine;
using UnityEngine.PostProcessing;

/// <summary>
/// Post processing manager.
/// </summary>
public class PostProcessingManager : MonoBehaviour
{
    /// <summary>
    /// Prototype to base all profiles off of
    /// </summary>
    [SerializeField]
    private PostProcessingProfile _profilePrototype;
    
    /// <summary>
    /// Applys post processing effects based on the level number
    /// </summary>
    /// <param name="levelNumber">Level number.</param>
    public void ApplyPostProcessing(int levelNumber, int maxLevels)
    {
        var effects = Camera.main.gameObject.AddComponent<PostProcessingBehaviour>();
        var profile = Instantiate(_profilePrototype);
        var settings = profile.colorGrading.settings;
        settings.basic.hueShift = DetermineHueShift(levelNumber, maxLevels);
        profile.colorGrading.settings = settings;
        effects.profile = profile;
    }

    /// <summary>
    /// Determines the hue shift.
    /// </summary>
    /// <returns>The hue shift.</returns>
    /// <param name="levelNumber">Level number.</param>
    /// <param name="maxLevels">Max levels.</param>
    private int DetermineHueShift(int levelNumber, int maxLevels)
    {
        const int maxShift = 180;
        var step = (float) (2 * maxShift) / maxLevels;
        var shift = (levelNumber - 1) * step * (float)Math.Pow(-1, levelNumber);
        return (int) Mathf.Clamp(shift, -maxShift, maxShift);
    }
}
