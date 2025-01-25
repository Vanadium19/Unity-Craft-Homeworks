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
        [SerializeField] private UnityEventReceiver _unityEvents;

        [Header("Move Controller")]
        [SerializeField] private Transform _startPoint;
        [SerializeField] private Transform _endPoint;

        [Header("Main Settings")]
        [SerializeField] private int _damage = 2;
        [SerializeField] private int _health = 5;
        [SerializeField] private float _speed = 3;
        [SerializeField] private float _pushForce = 25;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Snake>()
                .FromInstance(_snake)
                .AsSingle();

            Container.BindInterfacesTo<PointsMoveController>()
                .AsCached()
                .WithArguments(_startPoint.position, _endPoint.position)
                .NonLazy();

            Container.Bind<TransformMover>()
                .AsSingle()
                .WithArguments(_transform, _speed);
            
            Container.Bind<UnityEventReceiver>()
                .FromInstance(_unityEvents)
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
        }
    }
}