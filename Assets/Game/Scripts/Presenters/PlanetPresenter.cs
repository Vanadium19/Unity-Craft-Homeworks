using System;
using Game.Views;
using Modules.Planets;
using Modules.UI;
using UnityEngine;

namespace Game.Presenters
{
    public class PlanetPresenter : IPlanetPresenter
    {
        private readonly IPlanet _planet;
        private readonly PlanetView _planetView;
        private readonly PopupShower _popupShower;

        public event Action<Vector3> IncomeGathered;

        public PlanetPresenter(IPlanet planet,
            PlanetView planetView,
            PopupShower popupShower)
        {
            _planet = planet;
            _planetView = planetView;
            _popupShower = popupShower;
        }

        public void Initialize()
        {
            _planetView.OnHold += OnHold;
            _planetView.OnClicked += OnClick;
            _planet.OnIncomeTimeChanged += ChangeIncomeTime;

            _planetView.Initialize(_planet.IsUnlocked);
            _planetView.SetPrice(_planet.Price);

            if (!_planet.IsUnlocked)
                _planet.OnUnlocked += UnlockPlanet;
        }

        public void Dispose()
        {
            _planetView.OnHold -= OnHold;
            _planetView.OnClicked -= OnClick;
            _planet.OnIncomeTimeChanged -= ChangeIncomeTime;
        }

        private void GatherIncome()
        {
            _planet.GatherIncome();
            IncomeGathered?.Invoke(_planetView.CoinPosition);
        }

        private void OnHold()
        {
            _popupShower.OpenPopup(_planet);
        }

        private void OnClick()
        {
            if (!_planet.IsUnlocked)
                _planet.Unlock();

            if (_planet.IsIncomeReady)
                GatherIncome();
        }

        private void UnlockPlanet()
        {
            _planetView.Initialize(_planet.IsUnlocked);

            _planet.OnUnlocked -= UnlockPlanet;
        }
        
        private void ChangeIncomeTime(float time)
        {
            _planetView.SetProgress(_planet.IncomeProgress, time);
        }
    }
}