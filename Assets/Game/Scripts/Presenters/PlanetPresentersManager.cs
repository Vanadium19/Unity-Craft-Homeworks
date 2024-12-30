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
        private readonly PlanetPresenterFactory _factory;

        private readonly List<PlanetPresenter> _presenters = new();

        public PlanetPresentersManager(IPlanet[] planets,
            PlanetView[] planetViews,
            PlanetPresenterFactory factory)
        {
            if (planetViews.Length != planets.Length)
                throw new ArgumentException();

            _planets = planets;
            _planetViews = planetViews;
            _factory = factory;
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
            for (int i = 0; i < _planetViews.Length; i++)
                _presenters.Add(_factory.Create(_planets[i], _planetViews[i]));
        }
    }
}