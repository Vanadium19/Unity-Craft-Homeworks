using System;
using System.Collections.Generic;
using Game.Views;
using Modules.Planets;
using Modules.UI;
using UnityEngine;
using Zenject;

namespace Game.Presenters
{
    public class PlanetPresentersManager : IInitializable, IDisposable
    {
        private readonly IPlanet[] _planets;

        private readonly PlanetView[] _planetViews;
        private readonly PlanetPopup _planetPopup;

        private readonly ParticleAnimator _particleAnimator;
        private readonly Vector3 _moneyViewPosition;

        private readonly List<IPresenter> _presenters = new();

        public PlanetPresentersManager(IPlanet[] planets,
            PlanetView[] planetViews,
            PlanetPopup planetPopup,
            ParticleAnimator particleAnimator,
            MoneyView moneyView)
        {
            if (planetViews.Length != planets.Length)
                throw new ArgumentException();

            _planets = planets;

            _planetViews = planetViews;
            _planetPopup = planetPopup;

            _particleAnimator = particleAnimator;
            _moneyViewPosition = moneyView.CoinPosition;
        }

        public void Initialize()
        {
            CreatePresenters();

            foreach (var presenter in _presenters)
                presenter.Initialize();
        }

        public void Dispose()
        {
            foreach (var presenter in _presenters)
                presenter.Dispose();
        }

        private void CreatePresenters()
        {
            var planetPresenters = new List<IPlanetPresenter>();

            for (int i = 0; i < _planetViews.Length; i++)
            {
                planetPresenters.Add(new PlanetPresenter(_planets[i], _planetViews[i]));
                _presenters.Add(new PlanetIncomePresenter(_planets[i], _planetViews[i]));
            }

            _presenters.AddRange(planetPresenters);
            _presenters.Add(new PlanetPopupPresenter(planetPresenters, _planetPopup));
            _presenters.Add(new ParticleAnimatorPresenter(planetPresenters, _particleAnimator, _moneyViewPosition));
        }
    }
}