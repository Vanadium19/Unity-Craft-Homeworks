using System;
using Game.Views;
using Modules.Planets;
using Modules.UI;
using UnityEngine;

namespace Game.Presenters
{
    public class PlanetPresenter : IPlanetPresenter
    {
        private readonly PlanetView _planetView;
        private readonly SmartButton _button;
        private readonly IPlanet _planet;

        public event Action<IPlanet> PopupOpened;
        public event Action<Vector3> IncomeGathered;

        public PlanetPresenter(IPlanet planet,
            PlanetView planetView)
        {
            _planet = planet;
            _planetView = planetView;

            _button = _planetView.GetComponentInChildren<SmartButton>();
        }

        public void Initialize()
        {
            _button.OnHold += OnHold;
            _button.OnClick += OnClick;
            _planet.OnIncomeTimeChanged += ChangeIncomeTime;

            _planetView.Initialize(_planet.IsUnlocked);
            _planetView.SetPrice(_planet.Price);

            if (!_planet.IsUnlocked)
                _planet.OnUnlocked += UnlockPlanet;
        }

        public void Dispose()
        {
            _button.OnHold -= OnHold;
            _button.OnClick -= OnClick;
            _planet.OnIncomeTimeChanged -= ChangeIncomeTime;
        }

        private void GatherIncome()
        {
            _planet.GatherIncome();
            IncomeGathered?.Invoke(_planetView.CoinPosition);
        }

        private void OnHold()
        {
            PopupOpened?.Invoke(_planet);
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