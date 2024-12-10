using System;

namespace Modules
{
    //Don't modify
    public interface IScore
    {
        event Action<int> OnStateChanged;
        
        int Current { get; }
        void Add(int amount);
    }
}