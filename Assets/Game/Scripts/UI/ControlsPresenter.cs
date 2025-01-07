using System;
using Game.Scripts.App.SaveLoad;

namespace Game.Gameplay
{
    public sealed class ControlsPresenter : IControlsPresenter
    {
        private readonly IGameSaveLoader _gameSaveLoader;

        public ControlsPresenter(IGameSaveLoader gameSaveLoader)
        {
            _gameSaveLoader = gameSaveLoader;
        }

        public async void Save(Action<bool, int> callback)
        {
            (bool status, int version) result = await _gameSaveLoader.Save();

            callback.Invoke(result.status, result.version);
        }

        public async void Load(string versionText, Action<bool, int> callback)
        {
            var result = false;

            if (int.TryParse(versionText, out int version))
                result = await _gameSaveLoader.Load(version);

            callback.Invoke(result, version);
        }
    }
}