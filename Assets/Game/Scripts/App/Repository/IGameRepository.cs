using System.Collections.Generic;

namespace Game.Scripts.App.Repository
{
    public interface IGameRepository
    {
        public void SetState(Dictionary<string, string> state);
        public Dictionary<string, string> GetState();
    }
}