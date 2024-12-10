using Modules;
using UnityEngine;
using Zenject;

namespace Core
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