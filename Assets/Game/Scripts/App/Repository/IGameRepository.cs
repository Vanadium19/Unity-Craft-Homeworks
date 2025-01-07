using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Game.Scripts.App.Repository
{
    public interface IGameRepository
    {
        public UniTask<(bool, int)> SetState(Dictionary<string, string> state);

        public UniTask<(bool, Dictionary<string, string>)> GetState(int version);
    }
}