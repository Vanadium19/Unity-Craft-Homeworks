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
        private readonly IDifficulty _difficulty;
        
        private readonly ScoreView _scoreView;
        private readonly LevelView _levelView;

        private int _currentScore;

        public ProgressPresenter(ICoinsCollector coinsCollector,
            IDifficulty difficulty,
            ScoreView scoreView,
            LevelView levelView)
        {
            _coinsCollector = coinsCollector;
            _difficulty = difficulty;
            _scoreView = scoreView;
            _levelView = levelView;
        }

        public void Initialize()
        {
            _scoreView.SetScore(_currentScore.ToString());

            _difficulty.OnStateChanged += OnOnStateChanged;
            _coinsCollector.CoinCollected += OnCoinCollected;
        }

        public void Dispose()
        {
            _difficulty.OnStateChanged -= OnOnStateChanged;
            _coinsCollector.CoinCollected -= OnCoinCollected;
        }

        private void OnCoinCollected(ICoin coin)
        {
            _currentScore += coin.Score;

            _scoreView.SetScore($"Score: {_currentScore}");
        }

        private void OnOnStateChanged()
        {
            _levelView.SetLevel($"Level: {_difficulty.Current}/{_difficulty.Max}");
        }
    }
}