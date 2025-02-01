using UnityEngine;
using Zenject;

namespace Game.View.Installers
{
    public class EnvironmentViewInstaller : MonoInstaller
    {
        [SerializeField] private AttackView _attackView;

        public override void InstallBindings()
        {
            Container.Bind<AttackView>()
                .FromInstance(_attackView)
                .AsSingle();
        }
    }
}