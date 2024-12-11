using System;

namespace UseCases.System
{
    public interface ILevelManager
    {
        bool IsWin { get; }
        
        event Action LevelsEnded;
    }
}