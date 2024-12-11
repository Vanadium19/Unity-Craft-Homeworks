using System;
using Coins;
using Modules;
using UI;
using Zenject;

namespace Presenters
{
    public class ProgressPresenter : IInitializable, IDisposable
    {
        private readonly ICoinsCollector _coinsCollector;
        private readonly ScoreView _scoreView;

        private int _currentScore;

        public ProgressPresenter(ICoinsCollector coinsCollector, ScoreView scoreView)
        {
            _coinsCollector = coinsCollector;
            _scoreView = scoreView;
        }

        public void Initialize()
        {
            _scoreView.SetScore(_currentScore.ToString());
            
            _coinsCollector.CoinCollected += OnCoinCollected;
        }

        public void Dispose()
        {
            _coinsCollector.CoinCollected -= OnCoinCollected;
        }

        private void OnCoinCollected(ICoin coin)
        {
            _currentScore += coin.Score;
            
            _scoreView.SetScore(_currentScore.ToString());
        }
    }
}