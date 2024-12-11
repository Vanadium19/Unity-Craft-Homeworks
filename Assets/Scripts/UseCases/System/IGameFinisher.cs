using System;

namespace UseCases.System
{
    public interface IGameFinisher
    {
        event Action<bool> GameFinished;
    }
}