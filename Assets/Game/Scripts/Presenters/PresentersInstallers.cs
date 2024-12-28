using UnityEngine;
using Zenject;

namespace Game.Presenters
{
    [CreateAssetMenu(
        fileName = "PresentersInstallers",
        menuName = "Zenject/New PresentersInstallers"
    )]
    public sealed class PresentersInstallers : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<PlanetPresentersManager>()
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesTo<MoneyPresenter>()
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesTo<PlanetPopupPresenter>()
                .AsSingle()
                .NonLazy();
            
            Container.Bind<PopupShower>()
                .AsSingle()
                .NonLazy();
        }
    }
}