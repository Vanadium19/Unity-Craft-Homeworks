using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Game.Scripts.App.SaveLoad.Serializers.ComponentSerializers
{
    public abstract class ComponentSerializer<TService, TData> : IComponentSerializer
    {
        private string _key => typeof(TData).Name;

        public void Serialize(GameObject gameObject, IDictionary<string, string> state)
        {
            if (!gameObject.TryGetComponent(out TService service))
                return;

            TData data = Serialize(service);
            string json = JsonConvert.SerializeObject(data);

            state[_key] = json;
        }
        
        public void Deserialize(GameObject gameObject, IDictionary<string, string> state)
        {
            if (!gameObject.TryGetComponent(out TService service))
                return;
                
            if (!state.TryGetValue(_key, out string json))
                return;

            TData data = JsonConvert.DeserializeObject<TData>(json);

            Deserialize(service, data);
        }

        protected abstract TData Serialize(TService service);

        protected abstract void Deserialize(TService service, TData data);
    }
}