using Game.Components;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class MoveComponentsInstaller : Installer<Transform, float, float, float, MoveComponentsInstaller>
    {
        private readonly Transform _transform;
        private readonly float _jumpForce;
        private readonly float _jumpDelay;
        private readonly float _speed;

        public MoveComponentsInstaller(Transform transform, float jumpForce, float jumpDelay, float speed)
        {
            _transform = transform;
            _jumpForce = jumpForce;
            _jumpDelay = jumpDelay;
            _speed = speed;
        }

        public override void InstallBindings()
        {
            Container.Bind<Mover>()
                .AsSingle()
                .WithArguments(_speed);

            Container.BindInterfacesAndSelfTo<Jumper>()
                .AsSingle()
                .WithArguments(_jumpForce, _jumpDelay);
            
            Container.Bind<Rotater>()
                .AsSingle()
                .WithArguments(_transform);
        }
    }
}