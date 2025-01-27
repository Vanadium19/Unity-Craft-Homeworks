using Game.Scripts.View;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Game.Presenters
{
    public class EnvironmentPresenterInstaller : MonoInstaller
    {
        [SerializeField] private AttackView _attackView;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<AttackPresenter>()
                .AsSingle()
                .NonLazy();

            Container.Bind<AttackView>()
                .FromInstance(_attackView)
                .AsSingle();
        }
    }
}