using Modules;
using UnityEngine;

namespace Coins
{
    public interface ICoinSpawner
    {
        int CoinsCount { get; }
        
        bool TryRemoveCoin(Vector2Int position, out ICoin coin);
    }
}