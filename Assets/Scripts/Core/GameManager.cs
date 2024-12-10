using System;
using Modules;
using UnityEngine;
using Zenject;

namespace Core
{
    public class GameManager : IInitializable, IDisposable
    {
        private readonly IDifficulty _difficulty;
        private ISnake _snake;
        private CoinSpawner _coinSpawner;

        private int _coinCount;
        private int _currentDifficulty;

        public GameManager(IDifficulty difficulty, ISnake snake, CoinSpawner coinSpawner)
        {
            _difficulty = difficulty;
            _snake = snake;
            _coinSpawner = coinSpawner;
        }

        public void Initialize()
        {
            _snake.OnMoved += OnSnakeMoved;

            UpdateLevel();
        }

        public void Dispose()
        {
            _snake.OnMoved -= OnSnakeMoved;
        }

        private void OnSnakeMoved(Vector2Int position)
        {
            if (_coinSpawner.TryRemoveCoin(position, out ICoin coin))
            {
                _snake.Expand(coin.Bones);
                _coinCount++;
            }

            if (_coinCount == _difficulty.Current)
                UpdateLevel();
        }

        private void UpdateLevel()
        {
            _coinCount = 0;
            _difficulty.Next(out _currentDifficulty);
            _snake.SetSpeed(_currentDifficulty);
        }
    }
}