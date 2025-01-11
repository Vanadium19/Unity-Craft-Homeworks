using System.Collections.Generic;
using UnityEngine;

namespace SampleGame.App.SaveLoad.Serializers
{
    public interface IComponentSerializer
    {
        public void Serialize(GameObject gameObject, IDictionary<string, string> state);

        public void Deserialize(GameObject gameObject, IDictionary<string, string> state);
    }
}