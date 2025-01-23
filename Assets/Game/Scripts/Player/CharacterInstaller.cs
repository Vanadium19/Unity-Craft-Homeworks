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

        [Header("Main Settings")] [SerializeField] private int _health = 5;
        [SerializeField] private float _speed = 3f;

        [Header("Jump Settings")] [SerializeField] private JumpParams _jumpParams;


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
        }

        #region Debug

        private void OnDrawGizmos()
        {
            if (_jumpParams.Point == null)
                return;

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(_jumpParams.Point.position, _jumpParams.OverlapSize);
        }

        #endregion
    }
}