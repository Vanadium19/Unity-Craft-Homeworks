using System;
using Modules;
using SnakeGame;
using UnityEngine;
using Zenject;

namespace Core
{
    public class GameManager : IInitializable, IDisposable
    {
        private readonly IDifficulty _difficulty;
        private readonly IWorldBounds _worldBounds;
        private readonly ISnake _snake;
        private readonly ICoinSpawner _coinSpawner;

        private int _coinCount;
        private int _currentDifficulty;

        public GameManager(IDifficulty difficulty, ISnake snake, ICoinSpawner coinSpawner, IWorldBounds worldBounds)
        {
            _difficulty = difficulty;
            _snake = snake;
            _coinSpawner = coinSpawner;
            _worldBounds = worldBounds;
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
            if (!_worldBounds.IsInBounds(position))
            {
                _snake.SetActive(false);
                Debug.Log("Snake is out of bounds");
            }
            
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