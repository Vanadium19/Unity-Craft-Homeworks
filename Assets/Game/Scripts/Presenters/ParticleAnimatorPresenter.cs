using System.Collections.Generic;
using Modules.UI;
using UnityEngine;

namespace Game.Presenters
{
    public class ParticleAnimatorPresenter : IPresenter
    {
        private readonly IEnumerable<IPlanetPresenter> _planetPresenters;
        private readonly ParticleAnimator _particleAnimator;
        private readonly Vector3 _moneyViewPosition;

        public ParticleAnimatorPresenter(IEnumerable<IPlanetPresenter> planetPresenters,
            ParticleAnimator particleAnimator,
            Vector3 moneyViewPosition)
        {
            _planetPresenters = planetPresenters;
            _particleAnimator = particleAnimator;
            _moneyViewPosition = moneyViewPosition;
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