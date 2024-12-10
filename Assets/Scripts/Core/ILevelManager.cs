using System;

namespace Core
{
    public interface ILevelManager
    {
        event Action LevelsEnded;
    }
}