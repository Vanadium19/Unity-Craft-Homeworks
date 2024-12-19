using System;
using NUnit.Framework;
using UnityEngine;

namespace Modules.Planets
{
    public sealed class PlanetTests
    {
        private MoneyAdapterMock _moneyAdapter;
        private PlanetConfig _config;
        private Planet _planet;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _config = PlanetConfig.New(new PlanetConfig.CreateArgs
            {
                name = "Test",
                maxLevel = 10,
                startIncome = 10,
                endIncome = 100,
                incomeStep = 10,
                incomeDuration = 5,
                unlockPrice = 200,
                upgradePrice = 20
            });
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            ScriptableObject.DestroyImmediate(_config);
        }

        [SetUp]
        public void Setup()
        {
            _moneyAdapter = new MoneyAdapterMock();
            _planet = new Planet(_config, _moneyAdapter);
        }

        [Test]
        public void Instantiate()
        {
            //Arrange:
            _moneyAdapter.Money = 1000;

            //Assert:
            Assert.AreEqual(_planet.Name, "Test");
            Assert.IsTrue(_planet.CanUnlock);
            Assert.IsFalse(_planet.CanUpgrade);
            Assert.IsFalse(_planet.IsUnlocked);

            Assert.AreEqual(0, _planet.MinuteIncome);
            Assert.AreEqual(0, _planet.NextMinuteIncome);
            
            Assert.AreEqual(0, _planet.Level);
            Assert.AreEqual(10, _planet.MaxLevel);
            Assert.AreEqual(1, _planet.NextLevel);
            Assert.IsFalse(_planet.IsMaxLevel);

            Assert.AreEqual(200, _planet.Price);

            Assert.Zero(_planet.MinuteIncome);
        }

        [Test]
        public void WhenInstantiateWithNullMoneyAdapter_ThenThrowsException()
        {
            Assert.Catch<ArgumentNullException>(() =>
            {
                var _ = new Planet(_config, null);
            });
        }
        
        [Test]
        public void WhenInstantiateWithNullConfig_ThenThrowsException()
        {
            Assert.Catch<ArgumentNullException>(() =>
            {
                var _ = new Planet(null, _moneyAdapter);
            });
        }

        [Test]
        public void Unlock()
        {
            bool unlockEvent = false;

            //Arrange:
            _moneyAdapter.Money = 1000;
            _planet.OnUnlocked += () => unlockEvent = true;

            //Pre-assert:
            Assert.IsTrue(_planet.CanUnlock);

            //Act:
            bool success = _planet.Unlock();

            //Assert:
            Assert.IsTrue(success);
            Assert.IsTrue(unlockEvent);

            Assert.AreEqual(1, _planet.Level);
            Assert.AreEqual(2, _planet.NextLevel);

            Assert.IsFalse(_planet.CanUnlock);
            Assert.IsFalse(_planet.IsMaxLevel);
            Assert.IsTrue(_planet.CanUpgrade);

            Assert.AreEqual(40, _planet.Price);
        }

        [TestCase(1, ExpectedResult = 40)]
        [TestCase(2, ExpectedResult = 60)]
        [TestCase(3, ExpectedResult = 80)]
        [TestCase(4, ExpectedResult = 100)]
        [TestCase(5, ExpectedResult = 120)]
        [TestCase(6, ExpectedResult = 140)]
        [TestCase(7, ExpectedResult = 160)]
        [TestCase(8, ExpectedResult = 180)]
        [TestCase(9, ExpectedResult = 200)]
        [TestCase(10, ExpectedResult = Planet.UNDEFINED_PRICE)]
        public int GetUpgradePrice(int level)
        {
            //Arrange:
            _planet.IsUnlocked = true;
            _planet.Level = level;
            return _planet.Price;
        }
        
        [TestCase(0, ExpectedResult = 1)]
        [TestCase(1, ExpectedResult = 2)]
        [TestCase(2, ExpectedResult = 3)]
        [TestCase(3, ExpectedResult = 4)]
        [TestCase(4, ExpectedResult = 5)]
        [TestCase(5, ExpectedResult = 6)]
        [TestCase(6, ExpectedResult = 7)]
        [TestCase(7, ExpectedResult = 8)]
        [TestCase(8, ExpectedResult = 9)]
        [TestCase(9, ExpectedResult = 10)]
        [TestCase(10, ExpectedResult = Planet.UNDEFINED_LEVEL)]
        public int GetNextLevel(int level)
        {
            //Arrange:
            _planet.IsUnlocked = true;
            _planet.Level = level;
            return _planet.NextLevel;
        }

        [TestCase(1, ExpectedResult = 28)]
        [TestCase(2, ExpectedResult = 37)]
        [TestCase(3, ExpectedResult = 46)]
        [TestCase(4, ExpectedResult = 55)]
        [TestCase(5, ExpectedResult = 64)]
        [TestCase(6, ExpectedResult = 73)]
        [TestCase(7, ExpectedResult = 82)]
        [TestCase(8, ExpectedResult = 91)]
        [TestCase(9, ExpectedResult = 100)]
        [TestCase(10, ExpectedResult = 0)]
        public int GetNextMinuteIncome(int level)
        {
            _planet.IsUnlocked = true;
            _planet.Level = level;
            return _planet.NextMinuteIncome;
        }
        
        [TestCase(1, ExpectedResult = 19)]
        [TestCase(2, ExpectedResult = 28)]
        [TestCase(3, ExpectedResult = 37)]
        [TestCase(4, ExpectedResult = 46)]
        [TestCase(5, ExpectedResult = 55)]
        [TestCase(6, ExpectedResult = 64)]
        [TestCase(7, ExpectedResult = 73)]
        [TestCase(8, ExpectedResult = 82)]
        [TestCase(9, ExpectedResult = 91)]
        [TestCase(10, ExpectedResult = 100)]
        public int GetMinuteIncome(int level)
        {
            _planet.IsUnlocked = true;
            _planet.Level = level;
            return _planet.MinuteIncome;
        }

        [Test(ExpectedResult = false)]
        public bool WhenCanUnlockOnMaxLevel_ThenReturnFalse()
        {
            //Arrange:
            _moneyAdapter.Money = int.MaxValue;

            _planet.IsUnlocked = true;
            _planet.Level = _planet.MaxLevel;
            return _planet.CanUnlock;
        }
        
               
        [Test(ExpectedResult = false)]
        public bool WhenCanUnlockOrUpgradeOnMaxLevel_ThenReturnFalse()
        {
            //Arrange:
            _moneyAdapter.Money = int.MaxValue;
            
            _planet.IsUnlocked = true;
            _planet.Level = _planet.MaxLevel;
            return _planet.CanUnlockOrUpgrade;
        }

        [Test(ExpectedResult = false)]
        public bool WhenCanUpgradeOnMaxLevel_ThenReturnFalse()
        {
            //Arrange:
            _moneyAdapter.Money = int.MaxValue;

            _planet.IsUnlocked = true;
            _planet.Level = _planet.MaxLevel;
            return _planet.CanUpgrade;
        }

        [Test(ExpectedResult = Planet.UNDEFINED_PRICE)]
        public int GetPriceOnMaxLevel()
        {
            //Arrange:
            _planet.IsUnlocked = true;
            _planet.Level = _planet.MaxLevel;

            //Pre-assert:
            Assert.IsTrue(_planet.IsUnlocked);
            Assert.IsTrue(_planet.IsMaxLevel);

            return _planet.Price;
        }
    }
}