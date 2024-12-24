using System;
using System.Collections.Generic;
using Game.Views;
using Modules.Planets;
using Modules.UI;
using UnityEngine;
using Zenject;

namespace Game.Presenters
{
    public class PlanetsPresenter : IInitializable, IDisposable
    {
        private IPlanet[] _planets;
        private readonly SmartButton[] _buttons;
        private readonly PlanetPopup _planetPopup;

        private List<PlanetPresenter> _planetPresenters = new();
        private PlanetPopupPresenter _planetPopupPresenter;

        public PlanetsPresenter(IPlanet[] planets, SmartButton[] buttons, PlanetPopup planetPopup)
        {
            _planets = planets;
            _buttons = buttons;
            _planetPopup = planetPopup;

            if (_buttons.Length != _planets.Length)
                throw new ArgumentException();
        }


        public void Initialize()
        {
            for (int i = 0; i < _buttons.Length; i++)
            {
                var planetPresenter = new PlanetPresenter(_planets[i], _buttons[i]);
                planetPresenter.Initialize();
                _planetPresenters.Add(planetPresenter);
            }

            _planetPopupPresenter = new PlanetPopupPresenter(_planetPopup, _planetPresenters);
            _planetPopupPresenter.Initialize();
        }

        public void Dispose()
        {
            foreach (var presenter in _planetPresenters)
                presenter.Dispose();

            _planetPopupPresenter.Dispose();
        }
    }
}