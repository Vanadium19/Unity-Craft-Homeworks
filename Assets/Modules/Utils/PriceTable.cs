using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.Utils
{
    [Serializable]
    public sealed class PriceTable
    {
        [ReadOnly, SerializeField]
        private int[] _prices;

        [SerializeField]
        private int _multiplier;

        public PriceTable()
        {
        }
        
        public PriceTable(int multiplier, int maxLevel)
        {
            _multiplier = multiplier;
            this.EvaluatePrices(maxLevel);
        }

        public void OnValidate(int maxLevel)
        {
            EvaluatePrices(maxLevel);
        }

        private void EvaluatePrices(int maxLevel)
        {
            _prices = new int[maxLevel];

            for (int i = 0; i < maxLevel; i++) 
                _prices[i] = (i + 1) * _multiplier;
        }
        
        public int GetPrice(int level)
        {
            return _prices[level - 1];
        }
    }
}