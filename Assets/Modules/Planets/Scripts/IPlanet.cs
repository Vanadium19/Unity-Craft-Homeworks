using System;
using UnityEngine;

namespace Modules.Planets
{
    public interface IPlanet
    {
        event Action OnUnlocked;
        event Action<int> OnGathered;
        event Action<int> OnUpgraded;
        event Action<int> OnPopulationChanged;

        event Action<float> OnIncomeTimeChanged;
        event Action<bool> OnIncomeReady;
        event Action<int> OnIncomeChanged;

        string Name { get; }
        int Price { get; }

        bool CanUnlock { get; }
        bool CanUpgrade { get; }
        bool CanUnlockOrUpgrade { get; }
        
        int Level { get; }
        int NextLevel { get; }
        int MaxLevel { get; }
        bool IsMaxLevel { get; }

        bool IsUnlocked { get; }
        bool IsIncomeReady { get; }
        float IncomeProgress { get; }

        int MinuteIncome { get; }
        int NextMinuteIncome { get; }
        int Population { get; }

        bool Unlock();
        bool Upgrade();
        bool UnlockOrUpgrade();
        bool GatherIncome();

        Sprite GetIcon(bool unlocked);
    }
}