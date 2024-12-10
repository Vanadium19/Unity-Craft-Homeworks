using Modules;
using UnityEngine;

namespace Core
{
    public interface ICoinSpawner
    {
        bool TryRemoveCoin(Vector2Int position, out ICoin coin);
    }
}