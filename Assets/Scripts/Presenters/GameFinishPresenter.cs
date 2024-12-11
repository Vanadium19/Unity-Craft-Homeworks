using System;
using UnityEngine;
using UseCases.System;
using Zenject;

namespace Presenters
{
    public class GameFinishPresenter : IInitializable, IDisposable
    {
        private readonly IGameFinisher _gameFinisher;
        
        private readonly GameObject _winPopup;
        private readonly GameObject _losePopup;

        public GameFinishPresenter(IGameFinisher gameFinisher,
            GameObject winPopup,
            GameObject losePopup)
        {
            _gameFinisher = gameFinisher;
            _winPopup = winPopup;
            _losePopup = losePopup;
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
            if (isWin)
                _winPopup.SetActive(true);
            else
                _losePopup.SetActive(true);
        }
    }
}