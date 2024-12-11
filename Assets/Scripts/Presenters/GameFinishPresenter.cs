using System;
using SnakeGame;
using UnityEngine;
using UseCases.System;
using Zenject;

namespace Presenters
{
    public class GameFinishPresenter : IInitializable, IDisposable
    {
        private readonly IGameFinisher _gameFinisher;
        private readonly IGameUI _gameUI;

        public GameFinishPresenter(IGameFinisher gameFinisher, IGameUI gameUI)
        {
            _gameFinisher = gameFinisher;
            _gameUI = gameUI;
        }

        public void Initialize()
        {
            _gameFinisher.GameFinished += FinishGame;
        }

        public void Dispose()
        {
            _gameFinisher.GameFinished += FinishGame;
        }
        
        private void FinishGame(bool isWin)
        {
            _gameUI.GameOver(isWin);
        }
    }
}