using System;
using Modules;
using SnakeGame;
using UnityEngine;
using Zenject;

namespace Core
{
    public class GameFinisher : IInitializable, IDisposable
    {
        private readonly ISnake _snake;
        private readonly IWorldBounds _bounds;
        private readonly ILevelManager _levelManager;

        private readonly GameObject _winPopup;
        private readonly GameObject _losePopup;

        public GameFinisher(ISnake snake,
            IWorldBounds bounds,
            ILevelManager levelManager,
            GameObject winPopup,
            GameObject losePopup)
        {
            _snake = snake;
            _bounds = bounds;
            _levelManager = levelManager;

            _winPopup = winPopup;
            _losePopup = losePopup;
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

            if (_levelManager.IsWin)
                _winPopup.SetActive(true);
            else
                _losePopup.SetActive(true);
        }
    }
}