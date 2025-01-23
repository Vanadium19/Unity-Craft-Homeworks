using Game.Components;
using UnityEngine;
using Zenject;

namespace Game.Enemies
{
    public class TrapInstaller : MonoInstaller
    {
        [SerializeField] private Trap _trap;
        [SerializeField] private int _damage = 1;
        [SerializeField] private int _health = 1;

        public override void InstallBindings()
        {
            Container.Bind<Trap>()
                .FromInstance(_trap)
                .AsSingle();

            Container.Bind<Attacker>()
                .AsSingle()
                .WithArguments(_damage);

            Container.Bind<Health>()
                .AsSingle()
                .WithArguments(_health);
        }
    }
}