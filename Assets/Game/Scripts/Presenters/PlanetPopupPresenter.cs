using System.Collections.Generic;
using Game.Views;
using Modules.Planets;

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
            _planetPopup.PopupClosed += OnPopupClosed;
        }

        public void Dispose()
        {
            foreach (var presenter in _presenters)
                presenter.Opened -= OpenPlanet;

            _planetPopup.UpdateButtonClicked -= UpgradePlanet;
            _planetPopup.PopupClosed -= OnPopupClosed;
        }

        private void OpenPlanet(IPlanet planet)
        {
            _planetPopup.Open();
            _currentPlanet = planet;

            SetPlanetInfo();
            _planetPopup.SetName(planet.Name);

            if (!_currentPlanet.IsUnlocked)
                _currentPlanet.OnUnlocked += UnlockPlanet;

            _currentPlanet.OnUpgraded += UpdatePlanetInfo;
            _currentPlanet.OnPopulationChanged += ChangePopulation;
        }

        private void SetPlanetInfo()
        {
            _planetPopup.SetIcon(_currentPlanet.GetIcon(_currentPlanet.IsUnlocked));
            _planetPopup.SetLevel(_currentPlanet.Level, _currentPlanet.MaxLevel);
            _planetPopup.SetIncome(_currentPlanet.MinuteIncome);
            _planetPopup.SetPrice(_currentPlanet.Price);
            _planetPopup.EnableUpgradeButton(!_currentPlanet.IsMaxLevel);
        }
        
        private void UnlockPlanet()
        {
            SetPlanetInfo();

            _currentPlanet.OnUnlocked -= UnlockPlanet;
        }

        private void UpgradePlanet()
        {
            _currentPlanet.UnlockOrUpgrade();
        }

        private void UpdatePlanetInfo(int upgradedPlanet)
        {
            SetPlanetInfo();
        }

        private void ChangePopulation(int population)
        {
            _planetPopup.SetPopulation(population);
        }

        private void OnPopupClosed()
        {
            _currentPlanet.OnUpgraded -= UpdatePlanetInfo;
            _currentPlanet.OnPopulationChanged -= ChangePopulation;
        }
    }
}