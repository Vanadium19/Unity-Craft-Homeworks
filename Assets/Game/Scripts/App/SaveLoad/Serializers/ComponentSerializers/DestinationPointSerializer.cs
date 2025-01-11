using SampleGame.App.Data;
using SampleGame.Gameplay;

namespace SampleGame.App.SaveLoad.Serializers
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