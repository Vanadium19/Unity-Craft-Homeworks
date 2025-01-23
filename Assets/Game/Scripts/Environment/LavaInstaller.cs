using Game.Components;
using UnityEngine;
using Zenject;

namespace Game.Environment
{
    public class LavaInstaller : MonoInstaller
    {
        private const int Damage = int.MaxValue;

        [SerializeField] private Lava _lava;

        public override void InstallBindings()
        {
            Container.Bind<Lava>()
                .FromInstance(_lava)
                .AsSingle();

            Container.Bind<Attacker>()
                .AsSingle()
                .WithArguments(Damage);
        }
    }
}