using System;

namespace Game.Gameplay
{
    public sealed class ControlsPresenter : IControlsPresenter
    {
        public void Save(Action<bool, int> callback)
        {
            //TODO:
            callback.Invoke(false, -1);
        }

        public void Load(string versionText, Action<bool, int> callback)
        {
            //TODO:
            callback.Invoke(false, -1);
        }
    }
}