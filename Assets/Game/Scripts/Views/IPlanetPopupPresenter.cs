using System;
using Modules.Planets;
using UnityEngine;

namespace Game.Views
{
    public interface IPlanetPopupPresenter
    {
        public string Name { get; }
        public string Income { get; }
        public string Level { get; }
        public string Population { get; }
        public string Price { get; }
        public Sprite Icon { get; }
        public bool IsButtonActive { get; }
        public bool IsPlanetUnlocked { get; }
        
        public event Action OnUnlocked;
        public event Action OnUpgrated;
        public event Action<string> OnPopulationChanged;

        public void SetCurrentPlanet(IPlanet planet);

        public void UpgradePlanet();
        
        public void OnPopupClosed();
    }
}