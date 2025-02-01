using Game.Core.Components;
using Game.View;
using UnityEngine;
using Zenject;

namespace Game.Content.Enemies
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
        
        [Header("View")]
        [SerializeField] private DamageView _damageView;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Snake>()
                .FromInstance(_snake)
                .AsSingle();

            Container.Bind<TransformMover>()
                .AsSingle()
                .WithArguments(_transform, _speed);
            
            Container.Bind<Rotater>()
                .AsSingle()
                .WithArguments(_transform);

            Container.Bind<TargetPusher>()
                .AsSingle()
                .WithArguments(_pushForce);
            
            Container.BindInterfacesAndSelfTo<PushableComponent>()
                .AsSingle()
                .WithArguments(_stunDelay);
            
            Container.Bind<UnityEventReceiver>()
                .FromInstance(_unityEvents)
                .AsSingle();

            Container.Bind<Rigidbody2D>()
                .FromInstance(_rigidbody)
                .AsSingle();
            
            Container.Bind<Attacker>()
                .AsSingle()
                .WithArguments(_damage);

            Container.Bind<Health>()
                .AsSingle()
                .WithArguments(_health);
            
            //Presenters
            Container.BindInterfacesTo<DamagePresenter>()
                .AsSingle()
                .NonLazy();
            
            //View
            Container.Bind<DamageView>()
                .FromInstance(_damageView)
                .AsSingle();
        }
    }
}