using Modules;
using SnakeGame;
using Zenject;

namespace Installers
{
    public class ModulesInstaller : Installer<WorldBounds, ModulesInstaller>
    {
        private const int MaxLevel = 9;
        
        [Inject] private WorldBounds _worldBounds;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<Difficulty>()
                .AsSingle()
                .WithArguments(MaxLevel);

            Container.BindInterfacesTo<WorldBounds>()
                .FromInstance(_worldBounds)
                .AsSingle();

            Container.BindInterfacesTo<Score>()
                .AsSingle();
        }
    }
}