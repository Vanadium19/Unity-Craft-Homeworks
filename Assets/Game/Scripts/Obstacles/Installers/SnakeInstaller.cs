using Game.Components;
using Game.Obstacles.Enemies;
using UnityEngine;
using Zenject;

namespace Game.Obstacles.Installers
{
    public class SnakeInstaller : MonoInstaller
    {
        [SerializeField] private Snake _snake;
        [SerializeField] private Transform _transform;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private UnityEventReceiver _unityEvents;

        [Header("Main Settings")]
        [SerializeField] private int _damage = 2;
        [SerializeField] private int _health = 5;
        [SerializeField] private float _speed = 3;
        [SerializeField] private float _pushForce = 15;
        [SerializeField] private float _stunDelay = 0f;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Snake>()
                .FromInstance(_snake)
                .AsSingle();

            Container.Bind<TransformMover>()
                .AsSingle()
                .WithArguments(_transform, _speed);
            
            Container.Bind<UnityEventReceiver>()
                .FromInstance(_unityEvents)
                .AsSingle();
            
            Container.Bind<Rigidbody2D>()
                .FromInstance(_rigidbody)
                .AsSingle();
            
            Container.Bind<Rotater>()
                .AsSingle()
                .WithArguments(_transform);

            Container.Bind<Attacker>()
                .AsSingle()
                .WithArguments(_damage);

            Container.Bind<Health>()
                .AsSingle()
                .WithArguments(_health);

            Container.Bind<TargetPusher>()
                .AsSingle()
                .WithArguments(_pushForce);
            
            Container.BindInterfacesAndSelfTo<PushableComponent>()
                .AsSingle()
                .WithArguments(_stunDelay);
        }
    }
}