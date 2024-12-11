using Presenters;
using UI;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private ScoreView _scoreView;

        public override void InstallBindings()
        {
            Container.Bind<ScoreView>()
                .FromInstance(_scoreView)
                .AsSingle();

            Container.BindInterfacesTo<ProgressPresenter>()
                .AsSingle()
                .NonLazy();
        }
    }
}