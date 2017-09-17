using TMPro;
using Zenject;

public class Dependencies : MonoInstaller<Dependencies>
{
    public override void InstallBindings()
    {
        Container.Bind<GravityController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<CubeCollider>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ScoreTracker>().ToSelf().AsSingle();
        Container.Bind<LevelManager>().ToSelf().AsSingle().NonLazy();
        Container.Bind<SoundManager>().ToSelf().AsSingle();
        Container.Bind<LevelCamera>().FromComponentInHierarchy().AsSingle();
        Container.Bind<LevelIntroText>().FromComponentInHierarchy().AsSingle();
    }
}