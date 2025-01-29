using System;
using Game.Components.Interfaces;
using Game.View;
using Zenject;

namespace Game.Presenters
{
    public class AttackPresenter : IInitializable, IDisposable
    {
        private readonly IAttacker _target;
        private readonly AttackView _view;

        public AttackPresenter(IAttacker target, AttackView view)
        {
            _target = target;
            _view = view;
        }

        public void Initialize()
        {
            _target.Attacked += OnAttacked;
        }

        public void Dispose()
        {
            _target.Attacked -= OnAttacked;
        }

        private void OnAttacked()
        {
            _view.Attack();
        }
    }
}