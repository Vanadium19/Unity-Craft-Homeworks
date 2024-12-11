using Presenters;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class PresentersInstaller : Installer<GameObject, GameObject, PresentersInstaller>
    {
        private readonly GameObject _winPopup;
        private readonly GameObject _losePopup;

        public PresentersInstaller(GameObject winPopup, GameObject losePopup)
        {
            _winPopup = winPopup;
            _losePopup = losePopup;
        }
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ProgressPresenter>()
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesTo<GameFinishPresenter>()
                .AsSingle()
                .WithArguments(_winPopup, _losePopup)
                .NonLazy();
        }
    }
}