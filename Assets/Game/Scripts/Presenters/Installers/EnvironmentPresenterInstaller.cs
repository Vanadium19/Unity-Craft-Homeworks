using UnityEngine;
using Zenject;

namespace Game.Presenters.Installers
{
    [CreateAssetMenu(
        fileName = "EnvironmentPresenterInstaller",
        menuName = "Zenject/New EnvironmentPresenterInstaller"
    )]
    public class EnvironmentPresenterInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<AttackPresenter>()
                .AsSingle()
                .NonLazy();
        }
    }
}