using System;
using UseCases.Coins;
using Modules;
using Zenject;

namespace UseCases.System
{
    internal class LevelManager : IInitializable, IDisposable, ILevelManager
    {
        private readonly ICoinsCollector _coinsCollector;
        private readonly IDifficulty _difficulty;
        private readonly ISnake _snake;
        
        private bool _isWin;

        public event Action LevelsEnded;

        public LevelManager(ICoinsCollector coinsCollector,
            IDifficulty difficulty,
            ISnake snake)
        {
            _coinsCollector = coinsCollector;
            _difficulty = difficulty;
            _snake = snake;
        }

        public bool IsWin => _isWin;

        public void Initialize()
        {
            UpdateLevel();

            _coinsCollector.AllCoinCollected += UpdateLevel;
        }

        public void Dispose()
        {
            _coinsCollector.AllCoinCollected -= UpdateLevel;
        }

        private void UpdateLevel()
        {
            if (!_difficulty.Next(out int difficulty))
            {
                _isWin = true;
                LevelsEnded?.Invoke();
            }

            _snake.SetSpeed(difficulty);
        }
    }
}