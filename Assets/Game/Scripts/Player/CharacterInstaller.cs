using Game.Components;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class CharacterInstaller : MonoInstaller
    {
        [SerializeField] private Character _character;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Transform _transform;
        [SerializeField] private UnityEventReceiver _unityEvents;

        [Header("Main Settings")]
        [SerializeField] private int _health = 5;
        [SerializeField] private float _speed = 5f;

        [Header("Jump Settings")]
        [SerializeField] private float _jumpForce = 12;
        [SerializeField] private float _jumpDelay = 0.5f;

        [Header("Push Settings")]
        [SerializeField] private PushParams _pushParams;
        [SerializeField] private float _stunDelay = 0.12f;

        [Header("Ground Check Settings")]
        [SerializeField] private GroundCheckParams _groundCheckParams;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<Character>()
                .FromInstance(_character)
                .AsSingle();

            //Controllers
            ControllersInstaller.Install(Container);

            //Components
            Container.Bind<Rigidbody2D>()
                .FromInstance(_rigidbody)
                .AsSingle();
            
            Container.Bind<UnityEventReceiver>()
                .FromInstance(_unityEvents)
                .AsSingle();

            StateComponentsInstaller.Install(Container, _groundCheckParams, _stunDelay, _health);
            MoveComponentsInstaller.Install(Container, _transform, _jumpForce, _jumpDelay, _speed);
            AttackComponentsInstaller.Install(Container, _pushParams);
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