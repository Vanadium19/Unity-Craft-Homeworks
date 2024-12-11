using System;
using UseCases.Coins;
using Modules;
using SnakeGame;
using Zenject;

namespace Presenters
{
    public class ProgressPresenter : IInitializable, IDisposable
    {
        private readonly ICoinsCollector _coinsCollector;
        private readonly IDifficulty _difficulty;
        private IGameUI _gameUI;

        private int _currentScore;

        public ProgressPresenter(ICoinsCollector coinsCollector,
            IDifficulty difficulty,
            IGameUI gameUI)
        {
            _coinsCollector = coinsCollector;
            _difficulty = difficulty;
            _gameUI = gameUI;
        }

        public void Initialize()
        {
            _gameUI.SetScore(_currentScore.ToString());

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

            _gameUI.SetScore(_currentScore.ToString());
        }

        private void OnOnStateChanged()
        {
            _gameUI.SetDifficulty(_difficulty.Current, _difficulty.Max);
        }
    }
}