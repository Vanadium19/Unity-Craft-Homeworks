using System.Collections.Generic;
using Game.Scripts.App.Data;
using Modules.Entities;
using Newtonsoft.Json;
using UnityEngine;

namespace Game.Scripts.App.SaveLoad.Serializers
{
    public class EntitiesSerializer : IGameSerializer
    {
        private readonly EntityWorld _entityWorld;
        private readonly EntitySerializer _entitySerializer;

        public EntitiesSerializer(EntityWorld entityWorld, EntitySerializer entitySerializer)
        {
            _entityWorld = entityWorld;
            _entitySerializer = entitySerializer;
        }

        public void Serialize(IDictionary<string, string> state)
        {
            var entities = _entityWorld.GetAll();
            var data = new List<EntityData>(entities.Count);

            foreach (var entity in entities)
                data.Add(_entitySerializer.Serialize(entity));

            state.Add(nameof(EntityData), JsonConvert.SerializeObject(data.ToArray()));
        }

        public void Deserialize(IDictionary<string, string> state)
        {
            var json = state[nameof(EntityData)];
            var data = JsonConvert.DeserializeObject<EntityData[]>(json);
            
            _entityWorld.DestroyAll();

            foreach (var entityData in data)
            {
                var entity = _entityWorld.Spawn(entityData.Name,
                    entityData.Position,
                    entityData.Rotation,
                    entityData.Id);
                
                _entitySerializer.Deserialize(entity, entityData);
            }
        }
    }
}