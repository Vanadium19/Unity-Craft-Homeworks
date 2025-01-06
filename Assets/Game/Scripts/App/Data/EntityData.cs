using System;
using System.Collections.Generic;

namespace Game.Scripts.App.Data
{
    [Serializable]
    public struct EntityData
    {
        public string Name;
        public int Id;
        public Dictionary<string, string> Components;
    }
}