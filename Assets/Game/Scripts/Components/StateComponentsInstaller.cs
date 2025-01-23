using UnityEngine;
using Zenject;

namespace Game.Components
{
    public class StateComponentsInstaller : Installer<int, StateComponentsInstaller>
    {
        private readonly int _health;

        public StateComponentsInstaller(int health)
        {
            _health = health;
        }

        public override void InstallBindings()
        {
            Container.Bind<Health>()
                .AsSingle()
                .WithArguments(_health);
        }
    }
}