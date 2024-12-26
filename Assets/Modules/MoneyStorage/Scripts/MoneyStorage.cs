using System;
using Sirenix.OdinInspector;

namespace Modules.Money
{
    public sealed class MoneyStorage : IMoneyStorage
    {
        public event MoneyChangedDelegate OnMoneyChanged;
        public event MoneyEarnedDelegate OnMoneyEarned;
        public event MoneySpentDelegate ChangeMoney;

        [ShowInInspector, ReadOnly]
        public int Money => _money;

        private int _money;
        
        public MoneyStorage(int initialMoney)
        {
            if (initialMoney < 0)
                throw new ArgumentOutOfRangeException(nameof(initialMoney));
            
            _money = initialMoney;
        }
    
        [Button]
        public void Earn(in int amount)
        {
            if (amount == 0)
                return;

            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            int previousMoney = _money;
            
            _money += amount;
            this.OnMoneyEarned?.Invoke(_money, amount);
            this.OnMoneyChanged?.Invoke(_money, previousMoney);
        }
    
        [Button]
        public void Spend(in int amount)
        {
            if (amount == 0)
                return;

            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));
            
            int previousMoney = _money;

            _money -= amount;
            this.ChangeMoney?.Invoke(_money, amount);
            this.OnMoneyChanged?.Invoke(_money, previousMoney);
        }

        [Button]
        public void Change(in int money)
        {
            if (money < 0)
                throw new ArgumentOutOfRangeException(nameof(money));

            int previousMoney = _money;
            _money = money;
            this.OnMoneyChanged?.Invoke(_money, previousMoney);
        }

        public bool IsEnough(in int amount)
        {
            return _money >= amount;
        }
    }
}