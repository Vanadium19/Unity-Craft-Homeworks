using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp.Level.Spawners
{
    public class Pool<T> where T : MonoBehaviour
    {
        private Transform _poolContainer;
        private Transform _worldContainer;

        private Queue<T> _objects;
        private T _prefab;

        public Pool(Transform poolContainer, Transform worldContainer, T prefab)
        {
            _poolContainer = poolContainer;
            _worldContainer = worldContainer;
            _objects = new();
            _prefab = prefab;
        }

        public T Pull()
        {
            var spawnableObject = _objects.Count == 0 ? Spawn() : _objects.Dequeue();

            spawnableObject.gameObject.SetActive(true);
            spawnableObject.transform.SetParent(_worldContainer);

            return spawnableObject;
        }

        public void Push(T spawnableObject)
        {
            spawnableObject.gameObject.SetActive(false);
            spawnableObject.transform.SetParent(_poolContainer);

            _objects.Enqueue(spawnableObject);
        }

        private T Spawn()
        {
            return Object.Instantiate(_prefab);
        }
    }
}