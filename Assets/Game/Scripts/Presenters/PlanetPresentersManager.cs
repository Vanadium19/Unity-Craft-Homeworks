using System;
using Game.Views;
using Modules.Planets;
using Zenject;

namespace Game.Presenters
{
    public class PlanetPresentersManager : IInitializable, IDisposable
    {
        private readonly IPlanet[] _planets;
        private readonly PlanetView[] _planetViews;
        private readonly PlanetPresenterFactory _factory;

        private readonly PlanetPresenter[] _presenters;

        public PlanetPresentersManager(PlanetPresenterFactory factory,
            PlanetView[] planetViews,
            IPlanet[] planets)
        {
            if (planetViews.Length != planets.Length)
                throw new ArgumentException();

            _factory = factory;
            _planetViews = planetViews;
            _planets = planets;

            _presenters = new PlanetPresenter[planets.Length];
        }

        public void Initialize()
        {
            for (int i = 0; i < _planetViews.Length; i++)
            {
                _presenters[i] = _factory.Create(_planets[i], _planetViews[i]);
                _presenters[i].Initialize();
            }
        }

        public void Dispose()
        {
            foreach (var presenter in _presenters)
                presenter.Dispose();
        }
    }
}