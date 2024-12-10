using System;
using System.Collections.Generic;
using Modules;
using SnakeGame;
using UnityEngine;
using Zenject;

namespace Core
{
    public class CoinSpawner : IInitializable, IDisposable
    {
        private readonly IDifficulty _difficulty;
        private readonly IWorldBounds _bounds;
        private readonly Dictionary<Vector2Int, Coin> _spawnedCoins;

        private readonly Coin _coinPrefab;

        public CoinSpawner(Coin coinPrefab, IDifficulty difficulty, IWorldBounds bounds)
        {
            _coinPrefab = coinPrefab;
            _difficulty = difficulty;
            _bounds = bounds;

            _spawnedCoins = new Dictionary<Vector2Int, Coin>();
        }

        public void Initialize()
        {
            _difficulty.OnStateChanged += Spawn;
        }

        public void Dispose()
        {
            _difficulty.OnStateChanged -= Spawn;
        }

        public bool TryRemoveCoin(Vector2Int position, out ICoin coin)
        {
            var result = _spawnedCoins.Remove(position, out Coin spawnedCoin);

            coin = spawnedCoin;

            if (result)
                GameObject.Destroy(spawnedCoin.gameObject);

            return result;
        }

        private void Spawn()
        {
            Debug.Log("Spawn");

            for (int i = 0; i < _difficulty.Current; i++)
            {
                var position = GetPosition();
                var coin = GameObject.Instantiate(_coinPrefab, new Vector3(position.x, position.y, 0f),
                    Quaternion.identity);

                _spawnedCoins.Add(position, coin);
                coin.Generate();
            }
        }

        private Vector2Int GetPosition()
        {
            var position = _bounds.GetRandomPosition();

            while (_spawnedCoins.ContainsKey(position))
                position = _bounds.GetRandomPosition();

            return position;
        }
    }
}