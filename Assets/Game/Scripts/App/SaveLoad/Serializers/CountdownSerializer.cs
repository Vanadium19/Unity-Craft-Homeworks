using Game.Scripts.App.Data;
using SampleGame.Gameplay;

namespace Game.Scripts.App.SaveLoad.Serializers
{
    public class CountdownSerializer : GameSerializer<Countdown, CountdownData>
    {
        public CountdownSerializer(Countdown service) : base(service, nameof(Countdown)) { }
        
        protected override CountdownData Serialize(Countdown service)
        {
            return new CountdownData { CurrentTime = service.Current, Duration = service.Duration };
        }

        protected override void Deserialize(Countdown service, CountdownData data)
        {
            throw new System.NotImplementedException();
        }
    }
}