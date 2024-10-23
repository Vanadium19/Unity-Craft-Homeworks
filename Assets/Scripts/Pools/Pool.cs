using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp.Pools
{
    public class Pool<T> where T : MonoBehaviour
    {
        private Transform _container;

        private Queue<T> _objects;
        private T _prefab;

        public Pool(Transform container, T prefab)
        {
            _container = container;
            _objects = new();
            _prefab = prefab;
        }

        public virtual T Pull()
        {
            if (_objects.Count == 0)
                return Spawn();

            var spawnableObject = _objects.Dequeue();
            spawnableObject.gameObject.SetActive(true);

            return spawnableObject;
        }

        public virtual void Push(T spawnableObject)
        {
            spawnableObject.gameObject.SetActive(false);
            _objects.Enqueue(spawnableObject);
        }

        protected virtual T Spawn()
        {
            var spawnableObject = Object.Instantiate(_prefab);

            spawnableObject.transform.SetParent(_container);
            return spawnableObject;
        }
    }
}