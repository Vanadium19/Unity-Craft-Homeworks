using System.Collections.Generic;
using Game.Scripts.App.Data;
using Modules.Entities;
using Newtonsoft.Json;
using SampleGame.Gameplay;

namespace Game.Scripts.App.SaveLoad.Serializers
{
    public class EntitySerializer
    {
        public EntityData Serialize(Entity entity)
        {
            var components = new Dictionary<string, string>();

            foreach (var serializer in GetSerializers(entity))
                serializer.Serialize(components);

            return new EntityData
            {
                Name = entity.Name,
                Id = entity.Id,
                Position = entity.transform.position,
                Rotation = entity.transform.rotation,
                Components = components,
            };
        }

        public void Deserialize(Entity entity, EntityData entityData)
        {
            foreach (var serializer in GetSerializers(entity))
                serializer.Serialize(entityData.Components);
        }
        
        private IEnumerable<IGameSerializer> GetSerializers(Entity entity)
        {
            var serializers = new List<IGameSerializer>();
            
            if (entity.TryGetComponent(out Team team))
                serializers.Add(new TeamSerializer(team));

            if (entity.TryGetComponent(out Health health))
                serializers.Add(new HealthSerializer(health));

            if (entity.TryGetComponent(out Countdown countdown))
                serializers.Add(new CountdownSerializer(countdown));

            if (entity.TryGetComponent(out DestinationPoint point))
                serializers.Add(new DestinationPointSerializer(point));

            if (entity.TryGetComponent(out ResourceBag bag))
                serializers.Add(new ResourceBagSerializer(bag));

            return serializers;
        }
    }
}