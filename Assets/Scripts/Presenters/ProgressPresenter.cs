using System;
using UseCases.Coins;
using Modules;
using SnakeGame;
using Zenject;

namespace Presenters
{
    public class ProgressPresenter : IInitializable, IDisposable
    {
        private readonly IScore _score;
        private readonly IDifficulty _difficulty;
        private readonly IGameUI _gameUI;

        public ProgressPresenter(IScore score,
            IDifficulty difficulty,
            IGameUI gameUI)
        {
            _score = score;
            _difficulty = difficulty;
            _gameUI = gameUI;
        }

        public void Initialize()
        {
            _gameUI.SetScore(_score.Current.ToString());

            _difficulty.OnStateChanged += OnOnStateChanged;
            _score.OnStateChanged += OnCoinCollected;
        }

        public void Dispose()
        {
            _difficulty.OnStateChanged -= OnOnStateChanged;
            _score.OnStateChanged -= OnCoinCollected;
        }

        private void OnCoinCollected(int score)
        {
            _gameUI.SetScore(score.ToString());
        }

        private void OnOnStateChanged()
        {
            _gameUI.SetDifficulty(_difficulty.Current, _difficulty.Max);
        }
    }
}