using System.Collections.Generic;
using Game.Scripts.App.Data;
using Modules.Entities;
using SampleGame.Gameplay;

namespace Game.Scripts.App.SaveLoad.Serializers
{
    public class EntitySerializer : GameSerializer<Entity, EntityData>
    {
        private readonly List<IGameSerializer> _serializers;

        public EntitySerializer(Entity entity) : base(entity, entity.Id.ToString())
        {
            _serializers = new List<IGameSerializer>();

            SetSerializers(entity);
        }

        protected override EntityData Serialize(Entity service)
        {
            var components = new Dictionary<string, string>();

            foreach (var serializer in _serializers)
                serializer.Serialize(components);

            return new EntityData { Name = service.name, Id = service.Id, Components = components };
        }

        protected override void Deserialize(Entity service, EntityData data)
        {
            throw new System.NotImplementedException();
        }

        private void SetSerializers(Entity entity)
        {
            if (entity.TryGetComponent(out Team team))
                _serializers.Add(new TeamSerializer(team));

            if (entity.TryGetComponent(out Health health))
                _serializers.Add(new HealthSerializer(health));

            if (entity.TryGetComponent(out Countdown countdown))
                _serializers.Add(new CountdownSerializer(countdown));

            if (entity.TryGetComponent(out Damage damage))
                _serializers.Add(new DamageSerializer(damage));

            if (entity.TryGetComponent(out DestinationPoint point))
                _serializers.Add(new DestinationPointSerializer(point));

            if (entity.TryGetComponent(out MoveSpeed speed))
                _serializers.Add(new MoveSpeedSerializer(speed));

            if (entity.TryGetComponent(out ResourceBag bag))
                _serializers.Add(new ResourceBagSerializer(bag));
        }
    }
}