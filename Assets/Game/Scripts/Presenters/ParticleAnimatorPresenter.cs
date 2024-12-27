using System.Collections.Generic;
using Modules.UI;
using UnityEngine;

namespace Game.Presenters
{
    public class ParticleAnimatorPresenter : IPresenter
    {
        private readonly ParticleAnimator _particleAnimator;
        private readonly Vector3 _moneyViewPosition;
        private readonly IEnumerable<PlanetPresenter> _planetPresenters;

        public ParticleAnimatorPresenter(ParticleAnimator particleAnimator,
            Vector3 moneyViewPosition,
            IEnumerable<PlanetPresenter> planetPresenters)
        {
            _particleAnimator = particleAnimator;
            _moneyViewPosition = moneyViewPosition;
            _planetPresenters = planetPresenters;
        }

        public void Initialize()
        {
            foreach (var presenter in _planetPresenters)
                presenter.IncomeGathered += OnIncomeGathered;
        }

        public void Dispose()
        {
            foreach (var presenter in _planetPresenters)
                presenter.IncomeGathered -= OnIncomeGathered;
        }

        private void OnIncomeGathered(Vector3 planetCoinPosition)
        {
            _particleAnimator.Emit(planetCoinPosition, _moneyViewPosition);
        }
    }
}