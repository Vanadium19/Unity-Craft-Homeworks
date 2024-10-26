using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp.Level.Spawners
{
    public abstract class Pool<T> where T : MonoBehaviour
    {
        private readonly Transform _poolContainer;
        private readonly Transform _worldContainer;
        private readonly Queue<T> _objects;
        private readonly T _prefab;

        public Pool(Transform poolContainer, Transform worldContainer, T prefab)
        {
            _poolContainer = poolContainer;
            _worldContainer = worldContainer;
            _objects = new();
            _prefab = prefab;
        }

        public T Pull()
        {
            if (_objects.Count == 0)
                return Spawn();

            var spawnableObject = _objects.Dequeue();

            OnPulled(spawnableObject);

            return spawnableObject;
        }

        public void Push(T spawnableObject)
        {
            OnPushed(spawnableObject);

            _objects.Enqueue(spawnableObject);
        }

        protected virtual void OnPulled(T spawnableObject)
        {
            spawnableObject.gameObject.SetActive(true);
            spawnableObject.transform.SetParent(_worldContainer);
        }

        protected virtual void OnPushed(T spawnableObject)
        {
            spawnableObject.gameObject.SetActive(false);
            spawnableObject.transform.SetParent(_poolContainer);
        }

        protected abstract void OnSpawned(T spawnableObject);

        private T Spawn()
        {
            var spawnableObject = Object.Instantiate(_prefab, _worldContainer);

            OnSpawned(spawnableObject);

            return spawnableObject;
        }
    }
}