using System;
using Game.Content.Player;
using Zenject;

namespace Game.View
{
    public class PushPresenter : IInitializable, IDisposable
    {
        private readonly ICharacter _target;
        private readonly PushView _view;

        public PushPresenter(ICharacter target, PushView view)
        {
            _target = target;
            _view = view;
        }

        public void Initialize()
        {
            _target.Pushed += OnPushed;
            _target.Tossed += OnTossed;
        }

        public void Dispose()
        {
            _target.Pushed -= OnPushed;
            _target.Tossed -= OnTossed;
        }

        private void OnTossed()
        {
            _view.Toss();
        }

        private void OnPushed()
        {
            _view.Push();
        }
    }
}