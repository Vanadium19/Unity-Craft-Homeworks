using System;
using Game.Components;
using Game.Obstacles.Environment;
using Game.Presenters;
using Game.View;
using UnityEngine;
using Zenject;

namespace Game.Obstacles.Installers
{
    public class TrampolineInstaller : MonoInstaller
    {
        [SerializeField] private AttackView _attackView;
        [SerializeField] private UnityEventReceiver _unityEvents;
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

            Container.Bind<UnityEventReceiver>()
                .FromInstance(_unityEvents)
                .AsSingle();
            
            //Presenters
            Container.BindInterfacesTo<AttackPresenter>()
                .AsSingle()
                .NonLazy();
            
            //View
            Container.Bind<AttackView>()
                .FromInstance(_attackView)
                .AsSingle();
        }
    }
}