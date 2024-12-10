using System;
using Modules;
using Zenject;

namespace Core
{
    public class GameManager : IInitializable
    {
        private IDifficulty _difficulty;

        public GameManager(IDifficulty difficulty, ICoin coin)
        {
            _difficulty = difficulty;
        }

        public void Initialize()
        {
            _difficulty.Next(out var difficulty);
        }
    }
}