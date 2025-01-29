using System;
using Game.Components.Interfaces;
using Game.View;
using Zenject;

namespace Game.Presenters
{
    public class PushPresenter : IInitializable, IDisposable
    {
        private readonly IPusher _target;
        private readonly PushView _view;

        public PushPresenter(IPusher target, PushView view)
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