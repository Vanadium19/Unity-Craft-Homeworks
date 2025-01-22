using Zenject;

namespace Game.Player
{
    public class ControllersInstaller : Installer<ControllersInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<MoveController>()
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesTo<JumpController>()
                .AsSingle()
                .NonLazy();
        }
    }
}