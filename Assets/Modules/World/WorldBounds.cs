using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SnakeGame
{
    //Don't modify
    public sealed class WorldBounds : MonoBehaviour, IWorldBounds
    {
        [SerializeField]
        private int _minX = -8;

        [SerializeField]
        private int _maxX = 8;

        [SerializeField]
        private int _minY = -8;

        [SerializeField]
        private int _maxY = 8;

        public bool IsInBounds(Vector2Int position)
        {
            return position.x >= _minX && position.x <= _maxX &&
                   position.y >= _minY && position.y <= _maxY;
        }

        public Vector2Int GetRandomPosition()
        {
            int x = Random.Range(_minX + 1, _maxX);
            int y = Random.Range(_minY + 1, _maxY);

            return new Vector2Int(x, y);
        }

        private void OnDrawGizmos()
        {
            Handles.DrawWireCube(Vector3.zero, new Vector3(_maxX - _minX, _maxY - _minY, 0));
        }
    }
}