using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Modules.Entities
{
    //Don't modify
    public sealed class EntityWorld : SerializedMonoBehaviour
    {
        [SerializeField]
        private bool autoRun = true;

        [SerializeField]
        private EntityCatalog _catalog;

        [OdinSerialize]
        private Dictionary<EntityType, Transform> _containers;

        [FoldoutGroup("Debug")]
        [ShowInInspector, ReadOnly]
        private Dictionary<int, Entity> _entities;

        [FoldoutGroup("Debug")]
        [ShowInInspector, ReadOnly]
        private readonly Queue<int> _recycledIds = new();

        [FoldoutGroup("Debug")]
        [ShowInInspector, ReadOnly]
        private int _maxId;

        private void Awake()
        {
            if (this.autoRun)
                this.ScanEntities();
        }

        private void ScanEntities()
        {
            Entity[] entities = FindObjectsOfType<Entity>(includeInactive: true);
            int entityCount = entities.Length;

            _entities = new Dictionary<int, Entity>(entityCount);
            for (int i = 0; i < entityCount; i++)
                this.Add(entities[i]);
        }

        public bool Has(int id)
        {
            return _entities.ContainsKey(id);
        }
        
        public Entity Get(int id)
        {
            return _entities[id];
        }

        public bool TryGet(int id, out Entity entity)
        {
            return _entities.TryGetValue(id, out entity);
        }

        public IReadOnlyCollection<Entity> GetAll()
        {
            return _entities.Values;
        }

        [Button]
        public void Add(Entity entity, int id = -1)
        {
            if (_entities.ContainsKey(id))
                throw new Exception($"Entity with id {id} is already exists!");

            if (id == -1)
                id = this.NextId();

            entity.Id = id;
            _entities.Add(id, entity);
        }

        [Button]
        public bool Remove(Entity entity)
        {
            return entity && this.Remove(entity.Id);
        }

        public bool Remove(int entityId)
        {
            if (!_entities.Remove(entityId))
                return false;

            _recycledIds.Enqueue(entityId);
            return true;
        }

        public bool Remove(int entityId, out Entity entity)
        {
            if (!_entities.Remove(entityId, out entity))
                return false;

            _recycledIds.Enqueue(entityId);
            return true;
        }

        [Button]
        public Entity Spawn(string entityName, Vector3 position, Quaternion rotation, int id = -1)
        {
            if (string.IsNullOrEmpty(entityName))
                throw new ArgumentException(nameof(entityName));

            if (!_catalog.FindConfig(entityName, out EntityConfig config))
                throw new KeyNotFoundException($"Entity with name {entityName} is not found!");

            return this.Spawn(config, position, rotation, id);
        }

        [Button]
        public Entity Spawn(EntityConfig config, Vector3 position, Quaternion rotation, int id = -1)
        {
            if (!config)
                throw new ArgumentNullException(nameof(config));

            _containers.TryGetValue(config.Type, out Transform container);
            Entity entity = Instantiate(config.Prefab, position, rotation, container);
            this.Add(entity, id);
            return entity;
        }

        [Button]
        public void Destroy(Entity entity)
        {
            if (entity)
                this.Destroy(entity.Id);
        }

        public void Destroy(int entityId)
        {
            if (this.Remove(entityId, out Entity entity))
                GameObject.Destroy(entity.gameObject);
        }

        [Button]
        public void RemoveAll()
        {
            foreach (int entityId in _entities.Keys.ToArray())
                this.Remove(entityId);
        }

        [Button]
        public void DestroyAll()
        {
            foreach (int entityId in _entities.Keys.ToArray())
                this.Destroy(entityId);
        }

        private int NextId()
        {
            if (_recycledIds.TryDequeue(out int id))
                return id;

            do _maxId++;
            while (_entities.ContainsKey(_maxId));
            
            return _maxId;
        }
    }
}