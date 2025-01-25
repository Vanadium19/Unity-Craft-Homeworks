using System;
using Game.Components;
using Game.Obstacles.Environment;
using UnityEngine;
using Zenject;

namespace Game.Obstacles.Installers
{
    public class TrampolineInstaller : MonoInstaller
    {
        [SerializeField] private Trampoline _trampoline;
        [SerializeField] private float _pushForce = 50f;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Trampoline>()
                .FromInstance(_trampoline)
                .AsSingle();

            Container.Bind<TargetPusher>()
                .AsSingle()
                .WithArguments(_pushForce);
        }
    }
}