using System;

namespace Game.Scripts.App.Data
{
    [Serializable]
    public struct ResourceBagData
    {
        public int Type;
        public int Current;
        public int Capacity;
    }
}