using System;
using Modules;
using Zenject;

namespace Core
{
    public class LevelManager : IInitializable, IDisposable, ILevelManager
    {
        private readonly ICoinsCollector _coinsCollector;
        private readonly IDifficulty _difficulty;
        private readonly ISnake _snake;

        public event Action LevelsEnded;

        public LevelManager(ICoinsCollector coinsCollector,
            IDifficulty difficulty,
            ISnake snake)
        {
            _coinsCollector = coinsCollector;
            _difficulty = difficulty;
            _snake = snake;
        }

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
                LevelsEnded?.Invoke();

            _snake.SetSpeed(difficulty);
        }
    }
}