using Game.Scripts.App.Data;
using SampleGame.Gameplay;

namespace Game.Scripts.App.SaveLoad.Serializers
{
    public class DamageSerializer : GameSerializer<Damage, DamageData>
    {
        public DamageSerializer(Damage service) : base(service, nameof(DamageData))
        {
        }

        protected override DamageData Serialize(Damage service)
        {
            return new DamageData { Value = service.Value };
        }

        protected override void Deserialize(Damage service, DamageData data)
        {
            throw new System.NotImplementedException();
        }
    }
}