using System;
using Modules;
using UnityEngine;
using Zenject;

namespace UseCases.Coins
{
    public class CoinsCollector : IInitializable, IDisposable, ICoinsCollector
    {
        private readonly ICoinSpawner _coinSpawner;
        private readonly ISnake _snake;
        private readonly IScore _score;
        
        public event Action<ICoin> CoinCollected;
        public event Action AllCoinCollected;

        public CoinsCollector(ICoinSpawner coinSpawner, ISnake snake, IScore score)
        {
            _coinSpawner = coinSpawner;
            _snake = snake;
            _score = score;
        }

        public void Initialize()
        {
            _snake.OnMoved += OnSnakeMoved;
        }

        public void Dispose()
        {
            _snake.OnMoved -= OnSnakeMoved;
        }

        private void OnSnakeMoved(Vector2Int position)
        {
            if (_coinSpawner.TryRemoveCoin(position, out ICoin coin))
                CollectCoin(coin);

            if (_coinSpawner.CoinsCount == 0)
                AllCoinCollected?.Invoke();
        }
        
        private void CollectCoin(ICoin coin)
        {
            _snake.Expand(coin.Bones);
            _score.Add(coin.Score);
            CoinCollected?.Invoke(coin);
        }
    }
}