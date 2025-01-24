using Game.Components;
using Game.Obstacles.Environment;
using UnityEngine;
using Zenject;

namespace Game.Obstacles.Installers
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