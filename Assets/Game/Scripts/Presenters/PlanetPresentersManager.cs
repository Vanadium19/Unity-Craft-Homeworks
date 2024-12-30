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
        private readonly PopupShower _popupShower;
        private readonly PlanetPresentersMediator _mediator;

        private readonly List<PlanetPresenter> _presenters = new();

        public PlanetPresentersManager(IPlanet[] planets,
            PlanetView[] planetViews,
            PopupShower popupShower,
            PlanetPresentersMediator mediator)
        {
            if (planetViews.Length != planets.Length)
                throw new ArgumentException();

            _planets = planets;
            _planetViews = planetViews;
            _popupShower = popupShower;
            _mediator = mediator;
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
                _presenters.Add(new PlanetPresenter(_planets[i], _planetViews[i], _popupShower, _mediator));
        }
    }
}