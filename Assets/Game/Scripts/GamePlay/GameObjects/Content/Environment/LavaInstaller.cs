using Game.Core.Components;
using Game.View;
using UnityEngine;
using Zenject;

namespace Game.Content.Environment
{
    public class LavaInstaller : MonoInstaller
    {
        private const int Damage = int.MaxValue;

        [SerializeField] private AttackView _attackView;
        [SerializeField] private UnityEventReceiver _unityEvents;

        public override void InstallBindings()
        {
            //Main
            Container.BindInterfacesAndSelfTo<Lava>()
                .AsSingle()
                .NonLazy();

            //MonoBehaviors
            Container.Bind<UnityEventReceiver>()
                .FromInstance(_unityEvents)
                .AsSingle();

            //Components
            Container.BindInterfacesAndSelfTo<AttackComponent>()
                .AsSingle()
                .WithArguments(Damage);

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