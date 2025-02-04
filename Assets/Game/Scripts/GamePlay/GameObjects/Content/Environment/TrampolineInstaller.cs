using Game.Core.Components;
using Game.View;
using UnityEngine;
using Zenject;

namespace Game.Content.Environment
{
    public class TrampolineInstaller : MonoInstaller
    {
        [SerializeField] private Transform _trampoline;
        [SerializeField] private AttackView _attackView;
        [SerializeField] private UnityEventReceiver _unityEvents;
        [SerializeField] private float _pushForce = 50f;

        public override void InstallBindings()
        {
            //Main
            Container.BindInterfacesAndSelfTo<Trampoline>()
                .AsSingle()
                .NonLazy();

            //MonoBehaviors
            Container.Bind<UnityEventReceiver>()
                .FromInstance(_unityEvents)
                .AsSingle();

            Container.Bind<Transform>()
                .FromInstance(_trampoline)
                .AsSingle();

            //Components
            Container.Bind<TargetPushComponent>()
                .AsSingle()
                .WithArguments(_pushForce);

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