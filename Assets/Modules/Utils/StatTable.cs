using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.Utils
{
    [Serializable]
    public sealed class StatTable
    {
        [SerializeField]
        private int _startStatValue;

        [SerializeField]
        private int _endStatValue;

        [ReadOnly]
        [SerializeField]
        private int _statValueStep;

        [ReadOnly]
        [SerializeField]
        private int[] _statValues;

        public StatTable()
        {
        }

        public StatTable(int start, int end, int step, int maxLevel)
        {
            _startStatValue = start;
            _endStatValue = end;
            _statValueStep = step;
            this.OnValidate(maxLevel);
        }
        
        public void OnValidate(int maxLevel)
        {
            EvaluateStatValues(maxLevel);
        }

        private void EvaluateStatValues(int maxLevel)
        {
            _statValues = new int[maxLevel];

            int statValuesRange = _endStatValue - _startStatValue;
            _statValueStep = statValuesRange / maxLevel;

            for (int i = 0; i < maxLevel; i++)
            {
                float previousStatValue = i == 0 ? _startStatValue : _statValues[i - 1];
                int statValue = (int) Mathf.MoveTowards(previousStatValue, _endStatValue, _statValueStep);

                _statValues[i] = statValue;
            }
        }

        public int GetStatValue(int level)
        {
            return _statValues[level - 1];
        }
    }
}