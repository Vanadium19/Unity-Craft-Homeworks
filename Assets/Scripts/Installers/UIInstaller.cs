using SnakeGame;
using Zenject;

namespace Installers
{
    public class UIInstaller : Installer<GameUI, UIInstaller>
    {
        private readonly GameUI _gameUI;

        public UIInstaller(GameUI gameUI)
        {
            _gameUI = gameUI;
        }

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GameUI>()
                .FromInstance(_gameUI)
                .AsSingle();
        }
    }
}