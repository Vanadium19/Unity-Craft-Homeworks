using Presenters;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class PresentersInstaller : Installer<PresentersInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ProgressPresenter>()
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesTo<GameFinishPresenter>()
                .AsSingle()
                .NonLazy();
        }
    }
}