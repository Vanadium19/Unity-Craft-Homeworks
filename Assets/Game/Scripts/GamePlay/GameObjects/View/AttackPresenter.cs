using System;
using Game.Core.Components;
using Zenject;

namespace Game.View
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