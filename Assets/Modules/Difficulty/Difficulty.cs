using System;

namespace Modules
{
    //Don't modify
    public sealed class Difficulty : IDifficulty
    {
        public event Action OnStateChanged;
        
        public int Current => _current;
        public int Max => _max;

        private int _current;
        private readonly int _max;

        public Difficulty(int max)
        {
            _max = max;
        }

        public bool Next(out int difficulty)
        {
            if (_current == _max)
            {
                difficulty = default;
                return false;
            }

            _current++;
            this.OnStateChanged?.Invoke();
            
            difficulty = _current;
            return true;
        }
    }
}