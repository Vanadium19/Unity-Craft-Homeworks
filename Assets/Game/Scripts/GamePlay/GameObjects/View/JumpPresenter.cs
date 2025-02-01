using System;
using Game.Components.Interfaces;
using Game.View;
using Zenject;

namespace Game.Presenters
{
    public class JumpPresenter : IInitializable,IDisposable
    {
        private readonly IJumper _target;
        private readonly JumpView _view;

        public JumpPresenter(IJumper target, JumpView view)
        {
            _target = target;
            _view = view;
        }

        public void Initialize()
        {
            _target.Jumped += OnHealthChanged;
        }

        public void Dispose()
        {
            _target.Jumped -= OnHealthChanged;
        }

        private void OnHealthChanged()
        {
            _view.Jump();
        }
    }
}