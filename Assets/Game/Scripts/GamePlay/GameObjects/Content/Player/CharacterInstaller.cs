using Game.Core.Components;
using Game.View;
using UnityEngine;
using Zenject;

namespace Game.Content.Player
{
    public class CharacterInstaller : MonoInstaller
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Transform _transform;
        [SerializeField] private UnityEventReceiver _unityEvents;

        [Header("Main Settings")] [SerializeField] private int _health = 5;
        [SerializeField] private float _speed = 5f;

        [Header("Jump Settings")] [SerializeField] private float _jumpForce = 12;
        [SerializeField] private float _jumpDelay = 0.5f;

        [Header("Push Settings")] [SerializeField] private PushParams _pushParams;
        [SerializeField] private float _stunDelay = 0.12f;

        [Header("Ground Check Settings")] [SerializeField] private GroundCheckParams _groundCheckParams;

        //View
        [SerializeField] private PushView _pushView;
        [SerializeField] private JumpView _jumpView;
        [SerializeField] private DamageView _damageView;

        public override void InstallBindings()
        {
            //Main
            Container.BindInterfacesTo<Character>()
                .AsSingle()
                .NonLazy();

            //MonoBehaviors
            Container.Bind<Rigidbody2D>()
                .FromInstance(_rigidbody)
                .AsSingle();

            Container.Bind<Transform>()
                .FromInstance(_transform)
                .AsSingle();

            Container.Bind<UnityEventReceiver>()
                .FromInstance(_unityEvents)
                .AsSingle();

            //Components
            Container.BindInterfacesAndSelfTo<HealthComponent>()
                .AsSingle()
                .WithArguments(_health);

            Container.BindInterfacesAndSelfTo<GroundChecker>()
                .AsSingle()
                .WithArguments(_groundCheckParams);

            Container.BindInterfacesAndSelfTo<ForceComponent>()
                .AsSingle()
                .WithArguments(_stunDelay);

            Container.Bind<MoveComponent>()
                .AsSingle()
                .WithArguments(_speed);

            Container.BindInterfacesAndSelfTo<JumpComponent>()
                .AsSingle()
                .WithArguments(_jumpForce, _jumpDelay);

            Container.BindInterfacesAndSelfTo<RotateComponent>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<PlayerPushComponent>()
                .AsSingle()
                .WithArguments(_pushParams);

            //Presenters
            Container.BindInterfacesTo<DamagePresenter>()
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesTo<JumpPresenter>()
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesTo<PushPresenter>()
                .AsSingle()
                .NonLazy();

            //View
            Container.Bind<PushView>()
                .FromInstance(_pushView)
                .AsSingle();

            Container.Bind<DamageView>()
                .FromInstance(_damageView)
                .AsSingle();

            Container.Bind<JumpView>()
                .FromInstance(_jumpView)
                .AsSingle();
        }

        #region Debug

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;

            //Jump
            if (_groundCheckParams.Point == null)
                return;

            Gizmos.DrawWireCube(_groundCheckParams.Point.position, _groundCheckParams.OverlapSize);

            //Push
            if (_pushParams.Point == null)
                return;

            Gizmos.DrawWireSphere(_pushParams.Point.position, _pushParams.Radius);
        }

        #endregion
    }
}