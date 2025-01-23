using System;
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

        [Header("Main Settings")]
        [SerializeField] private int _health = 5;
        [SerializeField] private float _speed = 3f;

        [Header("Jump Settings")]
        [SerializeField] private JumpParams _jumpParams;

        [Header("Push Settings")]
        [SerializeField] private PushParams _pushParams;


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

            StateComponentsInstaller.Install(Container, _health);
            MoveComponentsInstaller.Install(Container, _jumpParams, _transform, _speed);
            AttackComponentsInstaller.Install(Container, _pushParams);
        }

        #region Debug

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;

            //Jump
            if (_jumpParams.Point == null)
                return;

            Gizmos.DrawWireCube(_jumpParams.Point.position, _jumpParams.OverlapSize);

            //Push
            if (_pushParams.Point == null)
                return;

            Gizmos.DrawWireSphere(_pushParams.Point.position, _pushParams.Radius);
        }

        #endregion
    }
}