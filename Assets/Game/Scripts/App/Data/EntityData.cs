using System;
using System.Collections.Generic;
using SampleGame.Common;

namespace SampleGame.App.Data
{
    [Serializable]
    public struct EntityData
    {
        public string Name;
        public int Id;
        public SerializedVector3 Position;
        public SerializedVector3 Rotation;
        public Dictionary<string, string> Components;
    }
}