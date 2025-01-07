using Cysharp.Threading.Tasks;

namespace Game.Scripts.App.SaveLoad
{
    public interface IGameSaveLoader
    {
        public UniTask<(bool, int)> Save();

        public UniTask<bool> Load(int version);
    }
}