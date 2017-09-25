using Zenject;

public class LevelDependencies : MonoInstaller<LevelDependencies>
{
	public override void InstallBindings()
	{
		Container.Bind<GravityController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<CubeCollider>().FromComponentInHierarchy().AsSingle();
		Container.Bind<LevelCamera>().FromComponentInHierarchy().AsSingle();
		Container.Bind<LevelManager>().ToSelf().AsSingle().NonLazy();
	}
}
