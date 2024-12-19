using System;
using NUnit.Framework;

namespace Modules.Money
{
    public sealed class MoneyStorageTests
    {
        [TestCase(-1)]
        [TestCase(-2)]
        [TestCase(-100)]
        public void WhenInstantiateWithInvalidMoneyThenException(int initialMoney)
        {
            Assert.Catch<ArgumentOutOfRangeException>(() =>
            {
                MoneyStorage _ = new MoneyStorage(initialMoney);
            });
        }
    }
}