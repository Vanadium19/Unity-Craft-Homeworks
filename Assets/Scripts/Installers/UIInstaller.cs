using Presenters;
using UI;
using Zenject;

namespace Installers
{
    public class UIInstaller : Installer<ScoreView, LevelView, UIInstaller>
    {
        private readonly ScoreView _scoreView;
        private readonly LevelView _levelView;

        public UIInstaller(ScoreView scoreView, LevelView levelView)
        {
            _scoreView = scoreView;
            _levelView = levelView;
        }

        public override void InstallBindings()
        {
            Container.Bind<ScoreView>()
                .FromInstance(_scoreView)
                .AsSingle();

            Container.Bind<LevelView>()
                .FromInstance(_levelView)
                .AsSingle();
        }
    }
}