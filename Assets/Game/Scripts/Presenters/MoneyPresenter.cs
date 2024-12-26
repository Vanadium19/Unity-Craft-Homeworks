using System;
using Game.Views;
using Modules.Money;
using UnityEngine;
using Zenject;

namespace Game.Presenters
{
    public class MoneyPresenter : IInitializable, IDisposable
    {
        private readonly IMoneyStorage _moneyStorage;
        private readonly MoneyView _view;

        public MoneyPresenter(IMoneyStorage moneyStorage, MoneyView view)
        {
            _moneyStorage = moneyStorage;
            _view = view;
        }

        public void Initialize()
        {
            _view.SetMoney(_moneyStorage.Money);
            
            _moneyStorage.OnMoneyChanged += SpendMoney;
        }

        public void Dispose()
        {
            _moneyStorage.OnMoneyChanged -= SpendMoney;
        }

        private void SpendMoney(int newValue, int prevValue)
        {
            _view.ChangeMoney(prevValue, newValue);
        }
    }
}