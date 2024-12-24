using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Views
{
    public class PlanetPopup : MonoBehaviour
    {
        [Header("Button")]
        [SerializeField] private Image _icon;
        [SerializeField] private Button _button;

        [Header("Info")]
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _population;
        [SerializeField] private TMP_Text _level;
        [SerializeField] private TMP_Text _income;
        [SerializeField] private TMP_Text _upgradePrice;

        public event Action PlanetUpgraded;

        private void OnEnable()
        {
            _button.onClick.AddListener(UpgradePlanet);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(UpgradePlanet);
        }

        public void SetName(string name)
        {
            _name.text = name;
        }

        public void ShowInfo(string population, string level, string income)
        {
            _population.text = population;
            _level.text = level;
            _income.text = income;
        }

        public void SetPrice(string price)
        {
            _upgradePrice.text = price;
        }

        private void UpgradePlanet()
        {
            PlanetUpgraded?.Invoke();
        }
    }
}