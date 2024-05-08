using Zenject;

public class GameInputInstaller : MonoInstaller<GameInputInstaller>
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GameInput>().AsSingle();
    }
}
