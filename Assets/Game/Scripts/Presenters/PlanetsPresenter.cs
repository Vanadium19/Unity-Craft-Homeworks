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
        private readonly IPlanet[] _planets;
        private readonly List<PlanetPresenter> _planetPresenters = new();
        private readonly List<PlanetIncomePresenter> _incomPresenters = new();
        
        private readonly PlanetView[] _planetViews;
        private readonly PlanetPopup _planetPopup;
        private readonly ParticleAnimator _particleAnimator;
        private readonly Vector3 _moneyViewPosition;

        private PlanetPopupPresenter _planetPopupPresenter;

        public PlanetsPresenter(IPlanet[] planets,
            PlanetView[] planetViews,
            PlanetPopup planetPopup,
            ParticleAnimator particleAnimator,
            MoneyView moneyView)
        {
            _planets = planets;
            _planetViews = planetViews;
            _planetPopup = planetPopup;
            _particleAnimator = particleAnimator;

            _moneyViewPosition = moneyView.CoinPosition;
            
            if (_planetViews.Length != _planets.Length)
                throw new ArgumentException();
        }


        public void Initialize()
        {
            for (int i = 0; i < _planetViews.Length; i++)
            {
                var planetPresenter = new PlanetPresenter(_planets[i], _planetViews[i], _particleAnimator, _moneyViewPosition);
                planetPresenter.Initialize();
                _planetPresenters.Add(planetPresenter);
                
                var incomePresenter = new PlanetIncomePresenter(_planets[i], _planetViews[i]);
                incomePresenter.Initialize();
                _incomPresenters.Add(incomePresenter);
            }

            _planetPopupPresenter = new PlanetPopupPresenter(_planetPopup, _planetPresenters);
            _planetPopupPresenter.Initialize();
        }

        public void Dispose()
        {
            for (int i = 0; i < _planetPresenters.Count; i++)
            {
                _planetPresenters[i].Dispose();
                _incomPresenters[i].Dispose();
            }

            _planetPopupPresenter.Dispose();
        }
    }
}