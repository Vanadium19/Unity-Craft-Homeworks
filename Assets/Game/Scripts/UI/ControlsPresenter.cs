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

        public void Save(Action<bool, int> callback)
        {
            //TODO:
            
            _gameSaveLoader.Save();
            
            callback.Invoke(false, -1);
        }

        public void Load(string versionText, Action<bool, int> callback)
        {
            //TODO:
            callback.Invoke(false, -1);
        }
    }
}