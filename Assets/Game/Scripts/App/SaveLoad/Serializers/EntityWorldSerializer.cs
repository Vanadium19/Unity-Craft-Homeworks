using System.Collections.Generic;
using Game.Scripts.App.Data;
using Game.Scripts.App.SaveLoad.Serializers.ComponentSerializers;
using Modules.Entities;
using Zenject;

namespace Game.Scripts.App.SaveLoad.Serializers
{
    public class EntityWorldSerializer : GameSerializer<EntityWorld, EntityData[]>
    {
        [Inject]
        private IComponentSerializer[] _componentSerializers;

        protected override EntityData[] Serialize(EntityWorld service)
        {
            var entities = service.GetAll();
            var data = new List<EntityData>(entities.Count);

            foreach (var entity in entities)
            {
                var components = new Dictionary<string, string>();
                var entityData = Serialize(entity, components);

                foreach (var serializer in _componentSerializers)
                    serializer.Serialize(entity.gameObject, components);

                data.Add(entityData);
            }

            return data.ToArray();
        }

        protected override void Deserialize(EntityWorld service, EntityData[] data)
        {
            service.DestroyAll();

            foreach (var entityData in data)
            {
                service.Spawn(entityData.Name,
                    entityData.Position,
                    entityData.Rotation,
                    entityData.Id);
            }

            foreach (var entityData in data)
            {
                foreach (var serializer in _componentSerializers)
                {
                    serializer.Deserialize(service.Get(entityData.Id).gameObject, entityData.Components);
                }
            }
        }

        private EntityData Serialize(Entity entity, Dictionary<string, string> components)
        {
            return new EntityData
            {
                Id = entity.Id,
                Name = entity.Name,
                Position = entity.transform.position,
                Rotation = entity.transform.rotation,
                Components = components,
            };
        }
    }
}