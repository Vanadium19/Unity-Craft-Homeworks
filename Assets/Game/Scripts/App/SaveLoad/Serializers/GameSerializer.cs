using System.Collections.Generic;
using Newtonsoft.Json;

namespace Game.Scripts.App.SaveLoad.Serializers
{
    public abstract class GameSerializer<TService, TData> : IGameSerializer
    {
        private readonly TService _service;
        private readonly string _key;

        protected GameSerializer(TService service, string key)
        {
            _service = service;
            _key = key;
        }

        public void Serialize(IDictionary<string, string> state)
        {
            TData data = Serialize(_service);
            string json = JsonConvert.SerializeObject(data);

            state[_key] = json;
        }

        public void Deserialize(IDictionary<string, string> state)
        {
            if (!state.TryGetValue(_key, out string json))
                return;

            TData data = JsonConvert.DeserializeObject<TData>(json);

            Deserialize(_service, data);
        }

        protected abstract TData Serialize(TService service);

        protected abstract void Deserialize(TService service, TData data);
    }
}