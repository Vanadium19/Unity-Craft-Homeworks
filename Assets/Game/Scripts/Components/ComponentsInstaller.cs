using UnityEngine;
using Zenject;

namespace Game.Components
{
    public class ComponentsInstaller : Installer<Rigidbody2D, float, float, int, ComponentsInstaller>
    {
        private readonly Rigidbody2D _rigidbody;
        private readonly float _speed;
        private readonly float _force;
        private readonly int _health;

        public ComponentsInstaller(Rigidbody2D rigidbody,
            float speed,
            float force,
            int health)
        {
            _rigidbody = rigidbody;
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
                .WithArguments(_speed);

            Container.Bind<Jumper>()
                .AsSingle()
                .WithArguments(_force);

            Container.Bind<Health>()
                .AsSingle()
                .WithArguments(_health);
        }
    }
}