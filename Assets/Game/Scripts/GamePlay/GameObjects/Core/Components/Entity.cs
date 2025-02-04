using UnityEngine;
using Zenject;

namespace Game.Core.Components
{
    public class Entity : MonoBehaviour, IEntity
    {
        [SerializeField] private GameObjectContext _context;

        private DiContainer _container;

        private void Awake()
        {
            _container = _context.Container;
        }

        public T Get<T>()
        {
            return _container.Resolve<T>();
        }

        public bool TryGet<T>(out T component) where T : class
        {
            component = _container.TryResolve<T>();

            return component != null;
        }
    }
}