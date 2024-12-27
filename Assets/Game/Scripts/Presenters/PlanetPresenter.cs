using System;
using Game.Views;
using Modules.Planets;
using Modules.UI;
using UnityEngine;

namespace Game.Presenters
{
    public class PlanetPresenter
    {
        private readonly PlanetView _planetView;
        private readonly SmartButton _button;
        private readonly IPlanet _planet;
        private readonly ParticleAnimator _particleAnimator;
        private readonly Vector3 _moneyViewPosition;

        public event Action<IPlanet> Opened;

        public PlanetPresenter(IPlanet planet,
            PlanetView planetView,
            ParticleAnimator particleAnimator,
            Vector3 moneyViewPosition)
        {
            _planet = planet;
            _planetView = planetView;
            
            _particleAnimator = particleAnimator;
            _moneyViewPosition = moneyViewPosition;

            _button = _planetView.GetComponentInChildren<SmartButton>();
        }

        public void Initialize()
        {
            _button.OnHold += OnHold;
            _button.OnClick += OnClick;
            
            _planetView.Initialize(_planet.IsUnlocked);
            _planetView.SetPrice(_planet.Price);
            
            if (!_planet.IsUnlocked)
                _planet.OnUnlocked += UnlockPlanet;
        }

        public void Dispose()
        {
            _button.OnHold -= OnHold;
            _button.OnClick -= OnClick;
        }

        private void OnHold()
        {
            Opened?.Invoke(_planet);
        }

        private void OnClick()
        {
            if (!_planet.IsUnlocked)
                _planet.Unlock();

            if (_planet.IsIncomeReady)
            {
                _planet.GatherIncome();
                _particleAnimator.Emit(_planetView.CoinPosition, _moneyViewPosition);
            }
        }

        private void UnlockPlanet()
        {
            _planetView.Initialize(_planet.IsUnlocked);
            
            _planet.OnUnlocked -= UnlockPlanet;
        }
    }
}