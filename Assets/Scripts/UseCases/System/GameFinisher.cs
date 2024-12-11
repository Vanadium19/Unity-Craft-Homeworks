using System;
using Modules;
using SnakeGame;
using UnityEngine;
using Zenject;

namespace UseCases.System
{
    public class GameFinisher : IInitializable, IDisposable, IGameFinisher
    {
        private readonly ISnake _snake;
        private readonly IWorldBounds _bounds;
        private readonly ILevelManager _levelManager;

        public event Action<bool> GameFinished;
        
        public GameFinisher(ISnake snake, IWorldBounds bounds, ILevelManager levelManager)
        {
            _snake = snake;
            _bounds = bounds;
            _levelManager = levelManager;
        }

        public void Initialize()
        {
            _levelManager.LevelsEnded += FinishGame;
            _snake.OnSelfCollided += FinishGame;
            _snake.OnMoved += OnSnakeMoves;
        }

        public void Dispose()
        {
            _levelManager.LevelsEnded -= FinishGame;
            _snake.OnSelfCollided -= FinishGame;
            _snake.OnMoved -= OnSnakeMoves;
        }

        private void OnSnakeMoves(Vector2Int position)
        {
            if (!_bounds.IsInBounds(position))
                FinishGame();
        }

        private void FinishGame()
        {
            _snake.SetActive(false);
            GameFinished?.Invoke(_levelManager.IsWin);
        }
    }
}