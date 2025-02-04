using Game.Core.Components;
using Game.View;
using UnityEngine;
using Zenject;

namespace Game.Content.Enemies
{
    public class SnakeInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _snake;
        [SerializeField] private Transform _transform;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private UnityEventReceiver _unityEvents;

        [Header("Main Settings")] [SerializeField] private int _damage = 2;
        [SerializeField] private int _health = 5;
        [SerializeField] private float _speed = 3;
        [SerializeField] private float _pushForce = 15;
        [SerializeField] private float _stunDelay = 0f;

        [Header("View")] [SerializeField] private DamageView _damageView;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<Snake>()
                .AsSingle()
                .NonLazy();

            Container.Bind<Transform>()
                .FromInstance(_transform)
                .AsSingle();

            Container.Bind<GameObject>()
                .FromInstance(_snake)
                .AsSingle();

            Container.BindInterfacesAndSelfTo<TransformMoveComponent>()
                .AsSingle()
                .WithArguments(_speed);

            Container.BindInterfacesAndSelfTo<RotateComponent>()
                .AsSingle();

            Container.Bind<TargetPushComponent>()
                .AsSingle()
                .WithArguments(_pushForce);

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

            Container.BindInterfacesAndSelfTo<HealthComponent>()
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