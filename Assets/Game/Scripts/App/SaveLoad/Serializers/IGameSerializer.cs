using System.Collections.Generic;

namespace SampleGame.App.SaveLoad.Serializers
{
    public interface IGameSerializer
    {
        public void Serialize(IDictionary<string, string> state);

        public void Deserialize(IDictionary<string, string> state);
    }
}