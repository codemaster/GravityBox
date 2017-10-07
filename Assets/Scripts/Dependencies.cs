using UnityEngine;
using Zenject;

/// <summary>
/// Dependencies for Zenject
/// </summary>
public class Dependencies : MonoInstaller<Dependencies>
{
    /// <summary>
    /// The sound manager prefab.
    /// </summary>
    [SerializeField]
    private SoundManager _soundManagerPrefab;

    /// <summary>
    /// The level config.
    /// </summary>
    [SerializeField]
    private LevelConfig _levelConfig;

    /// <summary>
    /// The sparks prefab
    /// </summary>
    [SerializeField]
    private ParticleSystem _sparks;

    /// <summary>
    /// Installs the bindings.
    /// </summary>
    public override void InstallBindings()
    {
        Container.BindMemoryPool<ParticleSystem, SparksPool>().FromComponentInNewPrefab(_sparks);
        Container.Bind<PostProcessingManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<ScoreTracker>().ToSelf().AsSingle();
        Container.Bind<TimeTracker>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
        Container.Bind<TimerDisplay>().FromComponentInHierarchy().AsSingle();
        Container.Bind<LevelConfig>().FromInstance(_levelConfig).AsSingle();
        Container.Bind<SoundManager>().FromComponentInNewPrefab(_soundManagerPrefab)
                 .AsSingle().NonLazy();
        Container.Bind<LevelIntroText>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<LevelOutroText>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<EndLevelButton>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<LevelLoader>().FromComponentInHierarchy().AsSingle();
    }
}