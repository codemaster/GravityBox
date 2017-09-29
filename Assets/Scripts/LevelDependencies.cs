using Zenject;

/// <summary>
/// Level dependencies.
/// </summary>
public class LevelDependencies : MonoInstaller<LevelDependencies>
{
    /// <summary>
    /// Installs the bindings.
    /// </summary>
	public override void InstallBindings()
	{
		Container.Bind<GravityController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<CubeCollider>().FromComponentInHierarchy().AsSingle();
		Container.Bind<LevelCamera>().FromComponentInHierarchy().AsSingle();
		Container.Bind<LevelManager>().ToSelf().AsSingle().NonLazy();
	}
}
