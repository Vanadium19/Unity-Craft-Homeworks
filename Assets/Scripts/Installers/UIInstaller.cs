using Presenters;
using UI;
using Zenject;

namespace Installers
{
    public class UIInstaller : Installer<ScoreView, LevelView, UIInstaller>
    {
        [Inject] private ScoreView _scoreView;
        [Inject] private LevelView _levelView;

        public override void InstallBindings()
        {
            Container.Bind<ScoreView>()
                .FromInstance(_scoreView)
                .AsSingle();

            Container.Bind<LevelView>()
                .FromInstance(_levelView)
                .AsSingle();

            Container.BindInterfacesTo<ProgressPresenter>()
                .AsSingle()
                .NonLazy();
        }
    }
}