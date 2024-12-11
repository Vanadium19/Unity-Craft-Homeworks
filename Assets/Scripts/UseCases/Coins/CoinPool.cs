using Modules;
using UnityEngine;
using Zenject;

namespace UseCases.Coins
{
    public class CoinPool : MonoMemoryPool<Vector3, Coin>
    {
        protected override void Reinitialize(Vector3 position, Coin coin)
        {
            coin.transform.position = position;
            coin.Generate();
        }
    }
}