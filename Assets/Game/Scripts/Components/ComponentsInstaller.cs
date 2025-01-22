using UnityEngine;
using Zenject;

namespace Game.Components
{
    public class ComponentsInstaller : Installer<Rigidbody2D, float, float, ComponentsInstaller>
    {
        private readonly Rigidbody2D _rigidbody;
        private readonly float _speed;
        private readonly float _force;

        public ComponentsInstaller(Rigidbody2D rigidbody,
            float speed,
            float force)
        {
            _speed = speed;
            _force = force;
            _rigidbody = rigidbody;
        }

        public override void InstallBindings()
        {
            Container.Bind<Rigidbody2D>()
                .FromInstance(_rigidbody)
                .AsSingle();

            Container.Bind<Mover>()
                .AsSingle()
                .WithArguments(_speed);

            Container.Bind<Jumper>()
                .AsSingle()
                .WithArguments(_force);
        }
    }
}