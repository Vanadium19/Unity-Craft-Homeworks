using System;
using Game.Views;
using Modules.Planets;
using Modules.UI;
using UniRx;
using UnityEngine;

namespace Game.Presenters
{
    public class PlanetPresenter
    {
        private PlanetIncomePresenter _incomePresenter;
        private readonly PlanetView _planetView;
        private readonly SmartButton _button;
        private readonly IPlanet _planet;

        private readonly ReactiveProperty<int> _population = new();

        public event Action<IPlanet, PlanetPresenter> Opened;

        public PlanetPresenter(IPlanet planet, PlanetView planetView)
        {
            _planet = planet;
            _planetView = planetView;

            _button = _planetView.GetComponentInChildren<SmartButton>();
        }

        public IReadOnlyReactiveProperty<int> Population => _population;

        public void Initialize()
        {
            _incomePresenter = new PlanetIncomePresenter(_planet, _planetView);
            _incomePresenter.Initialize();
            
            _button.OnHold += OnHold;
            _button.OnClick += OnClick;
            _planet.OnPopulationChanged += ChangePopulation;
            
            _planetView.Initialize(_planet.IsUnlocked);
            _planetView.SetPrice(_planet.Price);
            
            if (!_planet.IsUnlocked)
                _planet.OnUnlocked += UnlockPlanet;
        }

        public void Dispose()
        {
            _incomePresenter.Dispose();
            
            _button.OnHold -= OnHold;
            _button.OnClick -= OnClick;
            _planet.OnPopulationChanged -= ChangePopulation;
        }

        private void OnHold()
        {
            Opened?.Invoke(_planet, this);
        }

        private void OnClick()
        {
            if (!_planet.IsUnlocked)
                _planet.Unlock();

            if (_planet.IsIncomeReady)
                _planet.GatherIncome();
        }

        private void ChangePopulation(int population)
        {
            _population.Value = population;
        }

        private void UnlockPlanet()
        {
            _planetView.Initialize(_planet.IsUnlocked);
            
            _planet.OnUnlocked -= UnlockPlanet;
        }
    }
}