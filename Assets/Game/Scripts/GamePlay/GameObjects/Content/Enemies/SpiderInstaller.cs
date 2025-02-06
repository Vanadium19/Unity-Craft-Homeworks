using Game.Core.Components;
using Game.View;
using UnityEngine;
using Zenject;

namespace Game.Content.Enemies
{
    public class SpiderInstaller : MonoInstaller
    {
        [Header("Unity components")] [SerializeField] private GameObject _spider;
        [SerializeField] private Transform _transform;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private UnityEventReceiver _unityEvents;

        [Header("Move Settings")] [SerializeField] private Transform _startPoint;
        [SerializeField] private Transform _endPoint;
        [SerializeField] private float _speed = 5;

        [Header("Main Settings")] [SerializeField] private int _damage = 2;
        [SerializeField] private int _health = 3;
        [SerializeField] private float _pushForce = 20;
        [SerializeField] private float _stunDelay = 0f;

        [Header("View")] [SerializeField] private DamageView _damageView;

        public override void InstallBindings()
        {
            //Main
            Container.BindInterfacesAndSelfTo<Spider>()
                .AsSingle()
                .NonLazy();

            //MonoBehaviors
            Container.Bind<Transform>()
                .FromInstance(_transform)
                .AsSingle();

            Container.Bind<GameObject>()
                .FromInstance(_spider)
                .AsSingle();

            Container.Bind<UnityEventReceiver>()
                .FromInstance(_unityEvents)
                .AsSingle();

            Container.Bind<Rigidbody2D>()
                .FromInstance(_rigidbody)
                .AsSingle();

            //Components
            Container.BindInterfacesAndSelfTo<PatrolComponent>()
                .AsSingle()
                .WithArguments(_startPoint.position, _endPoint.position, _speed);

            Container.Bind<TargetPushComponent>()
                .AsSingle()
                .WithArguments(_pushForce);

            Container.BindInterfacesAndSelfTo<ForceComponent>()
                .AsSingle()
                .WithArguments(_stunDelay);

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