using Cysharp.Threading.Tasks;

namespace SampleGame.App.SaveLoad
{
    public interface IGameSaveLoader
    {
        public UniTask<(bool, int)> Save();

        public UniTask<bool> Load(int version);
    }
}