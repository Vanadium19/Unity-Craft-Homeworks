using UnityEngine;
using Zenject;

namespace Game.Components
{
    public class StateComponentsInstaller : Installer<GroundCheckParams, float, int, StateComponentsInstaller>
    {
        private readonly GroundCheckParams _groundCheckParams;
        private readonly float _stunDelay;
        private readonly int _health;

        public StateComponentsInstaller(GroundCheckParams groundCheckParams,
            float stunDelay,
            int health)
        {
            _groundCheckParams = groundCheckParams;
            _stunDelay = stunDelay;
            _health = health;
        }

        public override void InstallBindings()
        {
            Container.Bind<Health>()
                .AsSingle()
                .WithArguments(_health);

            Container.BindInterfacesAndSelfTo<GroundChecker>()
                .AsSingle()
                .WithArguments(_groundCheckParams);

            Container.BindInterfacesAndSelfTo<PushableComponent>()
                .AsSingle()
                .WithArguments(_stunDelay);
        }
    }
}