using System;
using Game.Core.Components;
using Zenject;

namespace Game.View
{
    public class DamagePresenter : IInitializable, IDisposable
    {
        private readonly IDamagable _target;
        private readonly DamageView _view;

        public DamagePresenter(IDamagable target, DamageView view)
        {
            _target = target;
            _view = view;
        }

        public void Initialize()
        {
            _target.HealthChanged += OnHealthChanged;
        }

        public void Dispose()
        {
            _target.HealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged(int value)
        {
            _view.TakeDamage();
        }
    }
}