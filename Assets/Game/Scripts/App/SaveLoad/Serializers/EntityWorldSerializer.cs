using System.Collections.Generic;
using Game.Scripts.App.Data;
using Modules.Entities;

namespace Game.Scripts.App.SaveLoad.Serializers
{
    public class EntityWorldSerializer : GameSerializer<EntityWorld, EntityData[]>
    {
        private readonly EntitySerializer _serializer;

        public EntityWorldSerializer(EntityWorld service, EntitySerializer serializer) : base(service, nameof(EntityData))
        {
            _serializer = serializer;
        }

        protected override EntityData[] Serialize(EntityWorld service)
        {
            var entities = service.GetAll();
            var data = new List<EntityData>(entities.Count);

            foreach (var entity in entities)
                data.Add(_serializer.Serialize(entity));

            return data.ToArray();
        }

        protected override void Deserialize(EntityWorld service, EntityData[] data)
        {
            service.DestroyAll();

            foreach (var entityData in data)
            {
                var entity = service.Spawn(entityData.Name,
                    entityData.Position,
                    entityData.Rotation,
                    entityData.Id);
                
                _serializer.Deserialize(entity, entityData);
            }
        }
    }
}