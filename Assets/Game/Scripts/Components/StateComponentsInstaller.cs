using UnityEngine;
using Zenject;

namespace Game.Components
{
    public class StateComponentsInstaller : Installer<GroundCheckParams, int, StateComponentsInstaller>
    {
        private readonly GroundCheckParams _groundCheckParams;
        private readonly int _health;

        public StateComponentsInstaller(GroundCheckParams groundCheckParams, int health)
        {
            _groundCheckParams = groundCheckParams;
            _health = health;
        }

        public override void InstallBindings()
        {
            Container.Bind<Health>()
                .AsSingle()
                .WithArguments(_health);
            
            Container.Bind<GroundChecker>()
                .AsSingle()
                .WithArguments((_groundCheckParams));
        }
    }
}