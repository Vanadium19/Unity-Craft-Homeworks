using UnityEngine;
using Zenject;

namespace Game.Components
{
    public class MoveComponentsInstaller : Installer<JumpParams, Transform, float, MoveComponentsInstaller>
    {
        private readonly JumpParams _jumpParams;
        private readonly Transform _transform;
        private readonly float _speed;

        public MoveComponentsInstaller(JumpParams jumpParams, Transform transform, float speed)
        {
            _jumpParams = jumpParams;
            _transform = transform;
            _speed = speed;
        }

        public override void InstallBindings()
        {
            Container.Bind<Mover>()
                .AsSingle()
                .WithArguments(_speed, _transform);

            Container.BindInterfacesAndSelfTo<Jumper>()
                .AsSingle()
                .WithArguments(_jumpParams);
        }
    }
}