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
            _button.OnHold += OnHold;
            _planet.OnPopulationChanged += ChangePopulation;
        }

        public void Dispose()
        {
            _button.OnHold -= OnHold;
            _planet.OnPopulationChanged -= ChangePopulation;
        }

        private void OnHold()
        {
            Opened?.Invoke(_planet, this);
        }

        private void ChangePopulation(int population)
        {
            _population.Value = population;
        }
    }
}