using System;
using Modules.Planets;
using Modules.UI;
using UnityEngine;

namespace Game.Presenters
{
    public class PlanetPresenter
    {
        private readonly IPlanet _planet;
        private readonly SmartButton _button;

        public event Action<IPlanet> Opened;

        public PlanetPresenter(IPlanet planet, SmartButton button)
        {
            _planet = planet;
            _button = button;
        }

        public void Initialize()
        {
            _button.OnHold += OnHold;
        }

        public void Dispose()
        {
            _button.OnHold -= OnHold;
        }

        private void OnHold()
        {
            Opened?.Invoke(_planet);
        }
    }
}