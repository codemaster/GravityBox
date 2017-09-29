using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

/// <summary>
/// Loads & unloads scenes (levels) as necessary
/// </summary>
public class LevelLoader : MonoBehaviour
{
	/// <summary>
	/// What level number are we on
	/// </summary>
	public int LevelNumber { get; private set; } = 1;

    /// <summary>
    /// Gets the total number of levels.
    /// </summary>
    /// <value>The total number of levels.</value>
    public int TotalLevels
    {
        get
        {
            return _levelConfig.LevelScenes.Count;
        }
    }

	/// <summary>
	/// The next level to load
	/// </summary>
	private int _nextLevelToLoad;

    /// <summary>
    /// The level config
    /// </summary>
    [Inject]
	private LevelConfig _levelConfig;

    /// <summary>
    /// Zenject scene loader
    /// </summary>
    [Inject]
    private ZenjectSceneLoader _zenSceneLoader;

    /// <summary>
    /// Restarts the game from the first level
    /// </summary>
    /// <returns>The restart.</returns>
    public IEnumerator Restart()
    {
        // Figure out our final scene
        var finalScene = _levelConfig.LevelScenes[LevelNumber - 1];

		// Reset our level numbers to be the default again
		LevelNumber = 1;
		_nextLevelToLoad = 1;

        // Load the first scene
		var loadAsyncOp = _zenSceneLoader.LoadSceneAsync(
			_levelConfig.LevelScenes[0],
			LoadSceneMode.Additive, null,
			LoadSceneRelationship.Child);

        yield return loadAsyncOp;

        // Unload the final scene
		var unloadAsyncOp = SceneManager.UnloadSceneAsync(finalScene);
		yield return unloadAsyncOp;
    }

	/// <summary>
	/// Loads the next level.
	/// </summary>
	public IEnumerator LoadNextLevel()
	{
        // Unload the previous scene, if we had one
        if (_nextLevelToLoad >= 1)
        {
            var unloadAsyncOp = SceneManager.UnloadSceneAsync(
                _levelConfig.LevelScenes[_nextLevelToLoad - 1]);
            yield return unloadAsyncOp;
        }

        ++_nextLevelToLoad;

        // Load the scene asychronously
		var loadAsyncOp = _zenSceneLoader.LoadSceneAsync(
			_levelConfig.LevelScenes[_nextLevelToLoad - 1],
			LoadSceneMode.Additive, null,
            LoadSceneRelationship.Child);
        
		// Start looking towards the next level
		LevelNumber = _nextLevelToLoad;

		yield return loadAsyncOp;
	}

    /// <summary>
    /// Start working with self & other objects
    /// </summary>
    public void Start()
    {
        StartCoroutine(LoadNextLevel());
    }
}
