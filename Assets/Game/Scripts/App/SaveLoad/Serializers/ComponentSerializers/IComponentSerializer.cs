using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.App.SaveLoad.Serializers.ComponentSerializers
{
    public interface IComponentSerializer
    {
        public void Serialize(GameObject gameObject, IDictionary<string, string> state);

        public void Deserialize(GameObject gameObject, IDictionary<string, string> state);
    }
}