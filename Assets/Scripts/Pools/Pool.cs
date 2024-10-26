using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp.Pools
{
    public abstract class Pool<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private Transform _worldContainer;
        [SerializeField] private T _prefab;

        private Queue<T> _objects = new();

        public T Pull()
        {
            var spawnableObject = _objects.Count == 0 ? Spawn() : _objects.Dequeue();

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
            spawnableObject.transform.SetParent(transform);
        }

        protected virtual void OnSpawned(T spawnableObject) { }

        private T Spawn()
        {
            var spawnableObject = Object.Instantiate(_prefab);

            OnSpawned(spawnableObject);

            return spawnableObject;
        }
    }
}