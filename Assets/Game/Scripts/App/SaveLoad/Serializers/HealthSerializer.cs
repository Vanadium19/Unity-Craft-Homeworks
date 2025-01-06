using Game.Scripts.App.Data;
using SampleGame.Gameplay;

namespace Game.Scripts.App.SaveLoad.Serializers
{
    public class HealthSerializer : GameSerializer<Health, HealthData>
    {
        public HealthSerializer(Health service) : base(service, nameof(HealthData)) { }

        protected override HealthData Serialize(Health service)
        {
            return new HealthData { CurrentHealth = service.Current };
        }

        protected override void Deserialize(Health service, HealthData data)
        {
            service.Current = data.CurrentHealth;
        }
    }
}