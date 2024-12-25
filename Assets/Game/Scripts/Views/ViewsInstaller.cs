using Modules.UI;
using UnityEngine;
using Zenject;

namespace Game.Views
{
    public sealed class ViewsInstaller : MonoInstaller
    {
        [SerializeField] private PlanetPopup _planetPopup;
        [SerializeField] private SmartButton[] _buttons;
        [SerializeField] private MoneyView _moneyView;

        public override void InstallBindings()
        {
            Container.Bind<PlanetPopup>()
                .FromInstance(_planetPopup)
                .AsSingle();

            Container.Bind<SmartButton[]>()
                .FromInstance(_buttons)
                .AsSingle();

            Container.Bind<MoneyView>()
                .FromInstance(_moneyView)
                .AsSingle();
        }
    }
}