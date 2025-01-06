using Game.Scripts.App.Data;
using Modules.Entities;
using SampleGame.Gameplay;
using UnityEngine;
using Zenject;

namespace Game.Scripts.App.SaveLoad.Serializers.ComponentSerializers
{
    public class TargetObjectSerializer : ComponentSerializer<TargetObject, TargetObjectData>
    {
        private const int FakeId = -1;

        [Inject]
        private EntityWorld _entityWorld;

        protected override TargetObjectData Serialize(TargetObject service)
        {
            if (service.Value is null)
                return new TargetObjectData { TargetId = FakeId };

            return new TargetObjectData { TargetId = service.Value.Id };
        }

        protected override void Deserialize(TargetObject service, TargetObjectData data)
        {
            if (data.TargetId == FakeId)
                return;

            if (_entityWorld.TryGet(data.TargetId, out Entity entity))
                service.Value = entity;
        }
    }
}