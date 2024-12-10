using Modules;
using UnityEngine;

namespace Core
{
    public interface ICoinSpawner
    {
        int CoinsCount { get; }
        
        bool TryRemoveCoin(Vector2Int position, out ICoin coin);
    }
}