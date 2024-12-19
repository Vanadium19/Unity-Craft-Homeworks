using Modules.Money;
using Modules.Planets;

namespace Game.Gameplay
{
    //Don't modify
    public sealed class MoneyAdapter : IMoneyAdapter
    {
        private readonly IMoneyStorage _moneyStorage;

        public MoneyAdapter(IMoneyStorage moneyStorage) => _moneyStorage = moneyStorage;
        
        public bool IsEnough(int money) => _moneyStorage.IsEnough(money);
        public void Spend(int money) => _moneyStorage.Spend(money);
        public void Earn(int money) => _moneyStorage.Earn(money);
    }
}