﻿using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

/// <summary>
/// Level config.
/// </summary>
[CreateAssetMenu]
public class LevelConfig : ScriptableObject
{
#if UNITY_EDITOR
    /// <summary>
    /// Scenes to assign (editor only)
    /// </summary>
    public SceneAsset[] LevelSceneAssets;
#endif

    /// <summary>
    /// List of the scene names
    /// </summary>
    [HideInInspector]
    public List<string> LevelScenes;

#if UNITY_EDITOR
    /// <summary>
    /// When the Unity inspector validates the assets,
    /// regenerate the list of names for the game to utilize.
    /// </summary>
    private void OnValidate()
    {
        LevelScenes.Clear();
        foreach (var sceneAsset in LevelSceneAssets)
        {
            if (null != sceneAsset)
            {
                LevelScenes.Add(sceneAsset.name);
            }
        }
    }
#endif
}
