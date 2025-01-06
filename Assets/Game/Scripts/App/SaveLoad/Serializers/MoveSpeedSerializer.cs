using Game.Scripts.App.Data;
using SampleGame.Gameplay;

namespace Game.Scripts.App.SaveLoad.Serializers
{
    public class MoveSpeedSerializer : GameSerializer<MoveSpeed, MoveSpeedData>
    {
        public MoveSpeedSerializer(MoveSpeed service) : base(service, nameof(MoveSpeed))
        {
        }

        protected override MoveSpeedData Serialize(MoveSpeed service)
        {
            return new MoveSpeedData { CurrentValue = service.Current };
        }

        protected override void Deserialize(MoveSpeed service, MoveSpeedData data)
        {
            throw new System.NotImplementedException();
        }
    }
}