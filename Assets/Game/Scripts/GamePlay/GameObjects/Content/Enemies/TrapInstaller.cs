using Game.Core.Components;
using UnityEngine;
using Zenject;

namespace Game.Content.Enemies
{
    public class TrapInstaller : MonoInstaller
    {
        [SerializeField] private Trap _trap;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private UnityEventReceiver _unityEvents;

        [SerializeField] private int _damage = 1;
        [SerializeField] private int _health = 1;
        [SerializeField] private float _stunDelay = 0f;

        public override void InstallBindings()
        {
            Container.Bind<Trap>()
                .FromInstance(_trap)
                .AsSingle();

            Container.BindInterfacesAndSelfTo<ForceComponent>()
                .AsSingle()
                .WithArguments(_stunDelay);

            Container.Bind<UnityEventReceiver>()
                .FromInstance(_unityEvents)
                .AsSingle();

            Container.Bind<Rigidbody2D>()
                .FromInstance(_rigidbody)
                .AsSingle();
            
            Container.Bind<AttackComponent>()
                .AsSingle()
                .WithArguments(_damage);

            Container.Bind<HealthComponent>()
                .AsSingle()
                .WithArguments(_health);
        }
    }
}