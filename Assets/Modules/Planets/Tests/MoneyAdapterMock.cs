namespace Modules.Planets
{
    public sealed class MoneyAdapterMock : IMoneyAdapter
    {
        public int Money { get; set; }
        
        public bool IsEnough(int money)
        {
            return this.Money >= money;
        }

        public void Spend(int money)
        {
            this.Money -= money;
        }

        public void Earn(int money)
        {
            this.Money += money;
        }
    }
}