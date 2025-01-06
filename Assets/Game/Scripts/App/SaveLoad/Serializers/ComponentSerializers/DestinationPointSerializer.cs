using Game.Scripts.App.Data;
using SampleGame.Gameplay;

namespace Game.Scripts.App.SaveLoad.Serializers.ComponentSerializers
{
    public class DestinationPointSerializer : ComponentSerializer<DestinationPoint, DestinationPointData>
    {
        protected override DestinationPointData Serialize(DestinationPoint service)
        {
            return new DestinationPointData { Value = service.Value };
        }

        protected override void Deserialize(DestinationPoint service, DestinationPointData data)
        {
            service.Value = data.Value;
        }
    }
}