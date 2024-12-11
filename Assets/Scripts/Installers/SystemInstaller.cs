using Core;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class SystemInstaller : Installer<GameObject, GameObject, SystemInstaller>
    {
        private readonly GameObject _winPopup;
        private readonly GameObject _losePopup;

        public SystemInstaller(GameObject winPopup, GameObject losePopup)
        {
            _winPopup = winPopup;
            _losePopup = losePopup;
        }

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<LevelManager>()
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesTo<GameFinisher>()
                .AsSingle()
                .WithArguments(_winPopup, _losePopup)
                .NonLazy();
        }
    }
}