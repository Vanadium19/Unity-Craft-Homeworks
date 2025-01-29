using Game.Components;
using Zenject;

namespace Game.Obstacles.Installers
{
    public class EnemyComponentsInstaller : Installer<int, int, EnemyComponentsInstaller>
    {
        private readonly int _damage;
        private readonly int _health;

        public EnemyComponentsInstaller(int damage, int health)
        {
            _damage = damage;
            _health = health;
        }

        public override void InstallBindings()
        {
            Container.Bind<Attacker>()
                .AsSingle()
                .WithArguments(_damage);

            Container.Bind<Health>()
                .AsSingle()
                .WithArguments(_health);
        }
    }
}