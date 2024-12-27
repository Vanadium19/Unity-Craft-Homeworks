using System.Collections.Generic;
using Game.Views;
using Modules.Planets;

namespace Game.Presenters
{
    public class PlanetPopupPresenter : IPresenter
    {
        private readonly IEnumerable<IPlanetPresenter> _presenters;
        private readonly PlanetPopup _planetPopup;

        private IPlanet _currentPlanet;

        public PlanetPopupPresenter(IEnumerable<IPlanetPresenter> presenters, PlanetPopup planetPopup)
        {
            _presenters = presenters;
            _planetPopup = planetPopup;
        }

        public void Initialize()
        {
            foreach (var presenter in _presenters)
                presenter.PopupOpened += OpenPlanet;

            _planetPopup.UpdateButtonClicked += UpgradePlanet;
            _planetPopup.PopupClosed += OnPopupClosed;
        }

        public void Dispose()
        {
            foreach (var presenter in _presenters)
                presenter.PopupOpened -= OpenPlanet;

            _planetPopup.UpdateButtonClicked -= UpgradePlanet;
            _planetPopup.PopupClosed -= OnPopupClosed;
        }

        private void OpenPlanet(IPlanet planet)
        {
            _planetPopup.Open();
            _currentPlanet = planet;

            SetPlanetInfo();
            _planetPopup.SetName(planet.Name);
            _planetPopup.SetIcon(_currentPlanet.GetIcon(_currentPlanet.IsUnlocked));

            if (!_currentPlanet.IsUnlocked)
                _currentPlanet.OnUnlocked += UnlockPlanet;

            _currentPlanet.OnUpgraded += UpdatePlanetInfo;
            _currentPlanet.OnPopulationChanged += ChangePopulation;
        }

        private void SetPlanetInfo()
        {
            _planetPopup.SetLevel(_currentPlanet.Level, _currentPlanet.MaxLevel);
            _planetPopup.SetIncome(_currentPlanet.MinuteIncome);
            _planetPopup.SetPrice(_currentPlanet.Price);
            _planetPopup.SetPopulation(_currentPlanet.Population);
            _planetPopup.EnableUpgradeButton(!_currentPlanet.IsMaxLevel);
        }

        private void UnlockPlanet()
        {
            SetPlanetInfo();
            _planetPopup.SetIcon(_currentPlanet.GetIcon(true));

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