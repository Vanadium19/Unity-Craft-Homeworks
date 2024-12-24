using System.Collections.Generic;
using Game.Views;
using Modules.Planets;
using UnityEngine;

namespace Game.Presenters
{
    public class PlanetPopupPresenter
    {
        private readonly PlanetPopup _planetPopup;
        private readonly IEnumerable<PlanetPresenter> _presenters;
        
        private IPlanet _currentPlanet;

        public PlanetPopupPresenter(PlanetPopup planetPopup, IEnumerable<PlanetPresenter> presenters)
        {
            _planetPopup = planetPopup;
            _presenters = presenters;
        }

        public void Initialize()
        {
            foreach (var presenter in _presenters)
                presenter.Opened += OpenPlanet;

            _planetPopup.UpdateButtonClicked += UpgradePlanet;
        }

        public void Dispose()
        {
            foreach (var presenter in _presenters)
                presenter.Opened -= OpenPlanet;
            
            _planetPopup.UpdateButtonClicked -= UpgradePlanet;
        }

        private void OpenPlanet(IPlanet planet)
        {
            _planetPopup.Open();
            _currentPlanet = planet;
            
            _planetPopup.SetName(planet.Name);
            _planetPopup.ShowInfo(planet.Population, planet.Level, planet.MinuteIncome);
            _planetPopup.SetPrice(planet.Price);
            _planetPopup.SetIcon(planet.GetIcon(planet.IsUnlocked));
        }

        private void UpgradePlanet()
        {
            _currentPlanet.UnlockOrUpgrade();
        }
    }
}