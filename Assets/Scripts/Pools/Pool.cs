using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp.Pools
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

        public virtual T Pull()
        {
            if (_objects.Count == 0)
                return Spawn();

            var spawnableObject = _objects.Dequeue();

            spawnableObject.gameObject.SetActive(true);
            spawnableObject.transform.SetParent(_worldContainer);

            return spawnableObject;
        }

        public virtual void Push(T spawnableObject)
        {
            spawnableObject.gameObject.SetActive(false);
            spawnableObject.transform.SetParent(_poolContainer);
            _objects.Enqueue(spawnableObject);
        }

        protected virtual T Spawn()
        {
            var spawnableObject = Object.Instantiate(_prefab);

            spawnableObject.transform.SetParent(_worldContainer);
            return spawnableObject;
        }
    }
}