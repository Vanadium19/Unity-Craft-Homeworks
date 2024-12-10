using System;
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

        private Coin _coinPrefab;

        public CoinSpawner(Coin coinPrefab, IDifficulty difficulty, IWorldBounds bounds)
        {
            _coinPrefab = coinPrefab;
            _difficulty = difficulty;
            _bounds = bounds;
        }

        public void Initialize()
        {
            _difficulty.OnStateChanged += Spawn;
        }

        public void Dispose()
        {
            _difficulty.OnStateChanged -= Spawn;
        }

        private void Spawn()
        {
            var position = _bounds.GetRandomPosition();

            for (int i = 0; i < _difficulty.Current; i++)
                GameObject.Instantiate(_coinPrefab, new Vector3(position.x, position.y, 0f), Quaternion.identity).Generate();
        }
    }
}