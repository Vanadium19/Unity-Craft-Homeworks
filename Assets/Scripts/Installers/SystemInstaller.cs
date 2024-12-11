using UseCases.System;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class SystemInstaller : Installer<SystemInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<LevelManager>()
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesTo<GameFinisher>()
                .AsSingle()
                .NonLazy();
        }
    }
}