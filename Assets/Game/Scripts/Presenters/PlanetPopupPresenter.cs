using System.Collections.Generic;
using Game.Views;
using Modules.Planets;
using UniRx;
using UnityEngine;

namespace Game.Presenters
{
    public class PlanetPopupPresenter
    {
        private readonly PlanetPopup _planetPopup;
        private readonly IEnumerable<PlanetPresenter> _presenters;

        private readonly CompositeDisposable _disposable = new();

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

        private void OpenPlanet(IPlanet planet, PlanetPresenter presenter)
        {
            _planetPopup.Open();
            _currentPlanet = planet;

            presenter.Population.Subscribe(_planetPopup.SetPopulation)
                .AddTo(_disposable);

            SetPlanetParams();
            
            if (!_currentPlanet.IsUnlocked)
                _currentPlanet.OnUnlocked += UnlockPlanet;

            _currentPlanet.OnUpgraded += UpgradePlanet;

            _planetPopup.SetName(planet.Name);
            _planetPopup.SetPrice(planet.Price);
        }

        private void UnlockPlanet()
        {
            SetPlanetParams();

            _currentPlanet.OnUnlocked -= UnlockPlanet;
        }

        private void SetPlanetParams()
        {
            _planetPopup.SetIcon(_currentPlanet.GetIcon(_currentPlanet.IsUnlocked));
            _planetPopup.SetLevel(_currentPlanet.Level, _currentPlanet.MaxLevel);
            _planetPopup.SetIncome(_currentPlanet.MinuteIncome);
        }

        private void UpgradePlanet()
        {
            _currentPlanet.UnlockOrUpgrade();
        }

        private void UpgradePlanet(int upgradedPlanet)
        {
            SetPlanetParams();
        }

        private void OnPopupClosed()
        {
            _currentPlanet.OnUpgraded += UpgradePlanet;
            _disposable.Clear();
        }
    }
}