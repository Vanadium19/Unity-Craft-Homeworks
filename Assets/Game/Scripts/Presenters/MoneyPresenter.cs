using System;
using Game.Views;
using Modules.Money;
using Modules.UI;
using UnityEngine;
using Zenject;

namespace Game.Presenters
{
    public class MoneyPresenter : IInitializable, IDisposable
    {
        private readonly ParticleAnimator _particleAnimator;
        private readonly IMoneyStorage _moneyStorage;
        private readonly MoneyView _view;

        public MoneyPresenter(ParticleAnimator particleAnimator,
            IMoneyStorage moneyStorage,
            MoneyView view)
        {
            _particleAnimator = particleAnimator;
            _moneyStorage = moneyStorage;
            _view = view;
        }

        public void Initialize()
        {
            _view.SetMoney(_moneyStorage.Money);

            _moneyStorage.ChangeMoney += SpendMoney;
        }

        public void Dispose()
        {
            _moneyStorage.ChangeMoney -= SpendMoney;
        }
        
        public void AddMoney(Vector3 planetPosition, int range)
        {
            var newValue = _moneyStorage.Money;

            _particleAnimator.Emit(planetPosition,
                _view.CoinPosition,
                onFinished: () => _view.AddMoney(newValue - range, newValue));
        }

        private void SpendMoney(int newValue, int range)
        {
            _view.SetMoney(newValue);
        }
    }
}