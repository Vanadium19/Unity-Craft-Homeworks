using UnityEngine;
using Zenject;

namespace Game.Components
{
    public class ComponentsInstaller : Installer<Rigidbody2D, Transform, float, float, int, ComponentsInstaller>
    {
        private readonly Rigidbody2D _rigidbody;
        private readonly Transform _transform;
        private readonly float _speed;
        private readonly float _force;
        private readonly int _health;

        public ComponentsInstaller(Rigidbody2D rigidbody,
            Transform transform,
            float speed,
            float force,
            int health)
        {
            _rigidbody = rigidbody;
            _transform = transform;
            _health = health;
            _speed = speed;
            _force = force;
        }

        public override void InstallBindings()
        {
            Container.Bind<Rigidbody2D>()
                .FromInstance(_rigidbody)
                .AsSingle();

            Container.Bind<Mover>()
                .AsSingle()
                .WithArguments(_speed, _transform);

            Container.Bind<Jumper>()
                .AsSingle()
                .WithArguments(_force);

            Container.Bind<Health>()
                .AsSingle()
                .WithArguments(_health);
        }
    }
}