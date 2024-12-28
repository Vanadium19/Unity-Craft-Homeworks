using System;
using System.Collections.Generic;
using Game.Views;
using Modules.Planets;
using UnityEngine;

namespace Game.Presenters
{
    public class PlanetPopupPresenter : IPlanetPopupPresenter
    {
        private IPlanet _currentPlanet;
        
        public string Name => _currentPlanet?.Name;
        public string Income => $"Income: {_currentPlanet?.MinuteIncome} / sec";
        public string Level => $"Level: {_currentPlanet?.Level}/{_currentPlanet?.MaxLevel}";
        public string Population => $"Population: {_currentPlanet?.Population}";
        public string Price => _currentPlanet?.Price.ToString();
        public Sprite Icon => _currentPlanet?.GetIcon(_currentPlanet.IsUnlocked);
        public bool IsButtonActive => !_currentPlanet.IsMaxLevel;
        public bool IsPlanetUnlocked => _currentPlanet.IsUnlocked;
        
        public event Action OnUnlocked;
        public event Action OnUpgrated;
        public event Action OnPopulationChanged;

        public void SetCurrentPlanet(IPlanet planet)
        {
            _currentPlanet = planet;
            
            if (!IsPlanetUnlocked)
                _currentPlanet.OnUnlocked += UnlockPlanet;
            
            _currentPlanet.OnUpgraded += UpgradePlanetInfo;
            _currentPlanet.OnPopulationChanged += ChangePopulation;
        }

        public void UpgradePlanet()
        {
            _currentPlanet.UnlockOrUpgrade();
        }

        public void OnPopupClosed()
        {
            _currentPlanet.OnUpgraded -= UpgradePlanetInfo;
            _currentPlanet.OnPopulationChanged -= ChangePopulation;
        }

        private void UnlockPlanet()
        {
            OnUnlocked?.Invoke();
            
            _currentPlanet.OnUnlocked -= UnlockPlanet;
        }

        private void UpgradePlanetInfo(int obj)
        {
            OnUpgrated?.Invoke();
        }

        private void ChangePopulation(int obj)
        {
            OnPopulationChanged?.Invoke();
        }
    }
}