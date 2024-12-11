using System;

namespace Core
{
    public interface ILevelManager
    {
        bool IsWin { get; }
        
        event Action LevelsEnded;
    }
}