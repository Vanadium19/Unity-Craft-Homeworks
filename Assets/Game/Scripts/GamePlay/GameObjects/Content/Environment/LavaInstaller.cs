using Game.Components;
using Game.Obstacles.Environment;
using Game.Presenters;
using Game.View;
using UnityEngine;
using Zenject;

namespace Game.Obstacles.Installers
{
    public class LavaInstaller : MonoInstaller
    {
        private const int Damage = int.MaxValue;

        [SerializeField] private AttackView _attackView;
        [SerializeField] private UnityEventReceiver _unityEvents;
        [SerializeField] private Lava _lava;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Lava>()
                .FromInstance(_lava)
                .AsSingle();

            Container.Bind<Attacker>()
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