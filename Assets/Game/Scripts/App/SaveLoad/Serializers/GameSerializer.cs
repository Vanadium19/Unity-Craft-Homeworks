using System.Collections.Generic;
using Newtonsoft.Json;
using Zenject;

namespace Game.Scripts.App.SaveLoad.Serializers
{
    public abstract class GameSerializer<TService, TData> : IGameSerializer
    {
        [Inject]
        private readonly TService _service;
        
        private string _key => typeof(TData).Name;

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