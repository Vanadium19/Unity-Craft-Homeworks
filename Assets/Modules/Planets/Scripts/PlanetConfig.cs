using Modules.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.Planets
{
    [CreateAssetMenu(
        fileName = "PlanetСonfig",
        menuName = "Modules/Planets/PlanetСonfig"
    )]
    public sealed class PlanetConfig : ScriptableObject
    {
        [field: SerializeField]
        public string Name { get; private set; }

        [field: SerializeField]
        public int PurchasePrice { get; private set; }

        [field: SerializeField]
        public int MaxLevel { get; private set; }

        [field: SerializeField]
        public float IncomeDuration { get; private set; } = 20;

        [field: SerializeField]
        public float PopulationPeriod { get; private set; } = 10;

        [SerializeField]
        private PriceTable _upgradePriceTable;

        [SerializeField]
        private StatTable _incomeTable;

        [Header("Meta")]
        [SerializeField, PreviewField]
        private Sprite _lockedIcon;

        [SerializeField, PreviewField]
        private Sprite _unlockedIcon;


        private void OnValidate()
        {
            _upgradePriceTable.OnValidate(MaxLevel);
            _incomeTable.OnValidate(MaxLevel);
        }

        public Sprite GetIcon(bool isUnlocked)
        {
            return isUnlocked ? _unlockedIcon : _lockedIcon;
        }

        public int GetUpgradePrice(int level)
        {
            return _upgradePriceTable.GetPrice(level);
        }

        public int GetIncome(int level)
        {
            return _incomeTable.GetStatValue(level);
        }

        public struct CreateArgs
        {
            public string name;
            public int maxLevel;

            public int startIncome;
            public int endIncome;

            public int incomeStep;
            public float incomeDuration;

            public int upgradePrice;
            public int unlockPrice;

            public int populationPeriod;

            public Sprite lockedIcon;
            public Sprite unlockedIcon;
        }

        public static PlanetConfig New(CreateArgs args)
        {
            PlanetConfig config = CreateInstance<PlanetConfig>();
            config.Name = args.name;
            config.MaxLevel = args.maxLevel;
            config.PurchasePrice = args.unlockPrice;
            config.IncomeDuration = args.incomeDuration;
            config.PopulationPeriod = args.populationPeriod;

            config._lockedIcon = args.lockedIcon;
            config._unlockedIcon = args.unlockedIcon;
            config._incomeTable = new StatTable(args.startIncome, args.endIncome, args.incomeStep, args.maxLevel);
            config._upgradePriceTable = new PriceTable(args.upgradePrice, args.maxLevel);
            return config;
        }
    }
}