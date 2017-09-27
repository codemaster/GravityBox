using UnityEngine;
using Zenject;

public class Dependencies : MonoInstaller<Dependencies>
{
    [SerializeField]
    private SoundManager _soundManagerPrefab;
    [SerializeField]
    private LevelConfig _levelConfig;
    
    public override void InstallBindings()
    {
        Container.Bind<ScoreTracker>().ToSelf().AsSingle();
        Container.Bind<TimeTracker>().ToSelf().AsSingle();
        Container.Bind<LevelConfig>().FromInstance(_levelConfig).AsSingle();
        Container.Bind<SoundManager>().FromComponentInNewPrefab(_soundManagerPrefab)
                 .AsSingle().NonLazy();
        Container.Bind<LevelIntroText>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<LevelOutroText>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<EndLevelButton>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<LevelLoader>().FromComponentInHierarchy().AsSingle();
    }
}