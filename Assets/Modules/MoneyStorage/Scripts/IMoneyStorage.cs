namespace Modules.Money
{
    public delegate void MoneyChangedDelegate(int newValue, int prevValue); 
    public delegate void MoneyEarnedDelegate(int newValue, int range);
    public delegate void MoneySpentDelegate(int newValue, int range);

    public interface IMoneyStorage
    {
        event MoneyChangedDelegate OnMoneyChanged;
        event MoneyEarnedDelegate OnMoneyEarned;
        event MoneySpentDelegate ChangeMoney;
        
        int Money { get; }

        void Earn(in int amount);
        void Spend(in int amount);
        void Change(in int money);
        bool IsEnough(in int amount);
    }
}