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
        Container.Bind<GravityController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<CubeCollider>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ScoreTracker>().ToSelf().AsSingle();
        Container.Bind<LevelConfig>().FromInstance(_levelConfig).AsSingle();
        Container.Bind<LevelManager>().ToSelf().AsSingle().NonLazy();
        Container.Bind<SoundManager>().FromComponentInNewPrefab(_soundManagerPrefab)
                 .AsSingle().NonLazy();
        Container.Bind<LevelCamera>().FromComponentInHierarchy().AsSingle();
        Container.Bind<LevelIntroText>().FromComponentInHierarchy().AsSingle();
        Container.Bind<LevelOutroText>().FromComponentInHierarchy().AsSingle();
        Container.Bind<EndLevelButton>().FromComponentInHierarchy().AsSingle();
    }
}