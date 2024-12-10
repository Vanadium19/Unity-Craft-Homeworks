using System;
using Sirenix.OdinInspector;

namespace Modules
{
    //Don't modify
    public sealed class Score : IScore
    {
        public event Action<int> OnStateChanged;
        
        public int Current => _current;
        
        private int _current;

        [Button]
        public void Add(int amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            if (amount == 0)
                return;

            _current += amount;
            OnStateChanged?.Invoke(_current);
        }
    }
}