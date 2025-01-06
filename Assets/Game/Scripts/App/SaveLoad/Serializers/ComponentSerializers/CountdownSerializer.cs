using Game.Scripts.App.Data;
using SampleGame.Gameplay;

namespace Game.Scripts.App.SaveLoad.Serializers.ComponentSerializers
{
    public class CountdownSerializer : ComponentSerializer<Countdown, CountdownData>
    {
        protected override CountdownData Serialize(Countdown service)
        {
            return new CountdownData { CurrentTime = service.Current };
        }

        protected override void Deserialize(Countdown service, CountdownData data)
        {
            service.Current = data.CurrentTime;
        }
    }
}