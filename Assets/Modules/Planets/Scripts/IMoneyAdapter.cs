namespace Modules.Planets
{
    public interface IMoneyAdapter
    {
        bool IsEnough(int money);
        void Spend(int money);
        void Earn(int money);
    }
}