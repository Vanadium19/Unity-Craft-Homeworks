using Modules;
using Player;
using PlayerInput;
using Zenject;

namespace Installers
{
    public class SnakeInstaller : Installer<Snake, SnakeInstaller>
    {
        [Inject] private Snake _snake;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<Snake>()
                .FromInstance(_snake)
                .AsSingle();

            Container.BindInterfacesTo<MoveInput>()
                .AsSingle();

            Container.BindInterfacesTo<SnakeMoveController>()
                .AsSingle()
                .NonLazy();
        }
    }
}