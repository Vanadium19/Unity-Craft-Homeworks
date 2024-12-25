using Modules.UI;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Game.Views
{
    public sealed class ViewsInstaller : MonoInstaller
    {
        [SerializeField] private PlanetPopup _planetPopup;
        [SerializeField] private MoneyView _moneyView;
        [SerializeField] private PlanetView[] _planetViews;

        public override void InstallBindings()
        {
            Container.Bind<PlanetPopup>()
                .FromInstance(_planetPopup)
                .AsSingle();

            Container.Bind<PlanetView[]>()
                .FromInstance(_planetViews)
                .AsSingle();

            Container.Bind<MoneyView>()
                .FromInstance(_moneyView)
                .AsSingle();
        }
    }
}