using Game.Scripts.App.Data;
using SampleGame.Gameplay;

namespace Game.Scripts.App.SaveLoad.Serializers
{
    public class DestinationPointSerializer : GameSerializer<DestinationPoint, DestinationPointData>
    {
        public DestinationPointSerializer(DestinationPoint service) : base(service, nameof(DestinationPointData))
        {
        }

        protected override DestinationPointData Serialize(DestinationPoint service)
        {
            return new DestinationPointData { Value = service.Value };
        }

        protected override void Deserialize(DestinationPoint service, DestinationPointData data)
        {
            throw new System.NotImplementedException();
        }
    }
}