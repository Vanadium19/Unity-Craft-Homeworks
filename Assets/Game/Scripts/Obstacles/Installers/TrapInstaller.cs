using Game.Components;
using Game.Obstacles.Enemies;
using UnityEngine;
using Zenject;

namespace Game.Obstacles.Installers
{
    public class TrapInstaller : MonoInstaller
    {
        [SerializeField] private Trap _trap;
        [SerializeField] private UnityEventReceiver _unityEvents;
        
        [SerializeField] private int _damage = 1;
        [SerializeField] private int _health = 1;

        public override void InstallBindings()
        {
            Container.Bind<Trap>()
                .FromInstance(_trap)
                .AsSingle();
            
            Container.Bind<UnityEventReceiver>()
                .FromInstance(_unityEvents)
                .AsSingle();

            Container.Bind<Attacker>()
                .AsSingle()
                .WithArguments(_damage);

            Container.Bind<Health>()
                .AsSingle()
                .WithArguments(_health);
        }
    }
}