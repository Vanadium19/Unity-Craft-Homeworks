using SampleGame.App.Data;
using SampleGame.Gameplay;

namespace SampleGame.App.SaveLoad.Serializers
{
    public class HealthSerializer : ComponentSerializer<Health, HealthData>
    {
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