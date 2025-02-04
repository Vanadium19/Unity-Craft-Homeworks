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
            Container.BindInterfacesAndSelfTo<Lava>()
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<AttackComponent>()
                .AsSingle()
                .WithArguments(Damage);

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