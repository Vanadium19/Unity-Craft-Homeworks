using System;
using System.Collections.Generic;
using Modules;
using SnakeGame;
using UnityEngine;
using Zenject;

namespace Core
{
    public class CoinSpawner : IInitializable, IDisposable, ICoinSpawner
    {
        private readonly IDifficulty _difficulty;
        private readonly IWorldBounds _bounds;
        private readonly CoinPool _coinPool;
        private readonly Dictionary<Vector2Int, Coin> _spawnedCoins;

        public CoinSpawner(IDifficulty difficulty, IWorldBounds bounds, CoinPool coinPool)
        {
            _difficulty = difficulty;
            _bounds = bounds;
            _coinPool = coinPool;

            _spawnedCoins = new Dictionary<Vector2Int, Coin>();
        }

        public void Initialize()
        {
            _difficulty.OnStateChanged += SpawnCoins;
        }

        public void Dispose()
        {
            _difficulty.OnStateChanged -= SpawnCoins;
        }

        public bool TryRemoveCoin(Vector2Int position, out ICoin coin)
        {
            var result = _spawnedCoins.Remove(position, out Coin spawnedCoin);

            coin = spawnedCoin;

            if (result)
                _coinPool.Despawn(spawnedCoin);

            return result;
        }

        private void SpawnCoins()
        {
            for (int i = 0; i < _difficulty.Current; i++)
                Spawn();
        }

        private void Spawn()
        {
            var position = GetPosition();
            var coin = _coinPool.Spawn(new Vector3(position.x, position.y, 0f));

            _spawnedCoins.Add(position, coin);
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