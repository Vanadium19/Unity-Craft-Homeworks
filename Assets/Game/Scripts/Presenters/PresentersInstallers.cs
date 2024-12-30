using Game.Views;
using Modules.Planets;
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
            Container.BindInterfacesAndSelfTo<MoneyPresenter>()
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesTo<PlanetPopupPresenter>()
                .AsSingle()
                .NonLazy();

            Container.Bind<PopupShower>()
                .AsSingle()
                .NonLazy();

            Container.Bind<PlanetPresentersMediator>()
                .AsSingle()
                .NonLazy();

            Container.BindFactory<IPlanet, PlanetView, PlanetPresenter, PlanetPresenterFactory>()
                .AsSingle();
            
            Container.BindInterfacesTo<PlanetPresentersManager>()
                .AsSingle()
                .NonLazy();
        }
    }
}