using Game.Views;
using Modules.Planets;

namespace Game.Presenters
{
    public class PopupShower
    {
        private readonly IPlanetPopupPresenter _presenter;
        private readonly PlanetPopup _popup;

        public PopupShower(IPlanetPopupPresenter presenter, PlanetPopup popup)
        {
            _presenter = presenter;
            _popup = popup;
        }

        public void OpenPopup(IPlanet planet)
        {
            _presenter.SetCurrentPlanet(planet);
            _popup.Open();
        }
    }
}