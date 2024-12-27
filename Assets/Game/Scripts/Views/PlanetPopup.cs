using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Views
{
    public class PlanetPopup : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private GameObject _pricePanel;
        
        [Header("Button")]
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private Button _closeButton;

        [Header("Info")]
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _population;
        [SerializeField] private TMP_Text _level;
        [SerializeField] private TMP_Text _income;
        [SerializeField] private TMP_Text _upgradePrice;

        public event Action UpdateButtonClicked;
        public event Action PopupClosed;

        private void OnEnable()
        {
            _upgradeButton.onClick.AddListener(UpgradePlanet);
            _closeButton.onClick.AddListener(ClosePopup);
        }

        private void OnDisable()
        {
            _upgradeButton.onClick.RemoveListener(UpgradePlanet);
            _closeButton.onClick.RemoveListener(ClosePopup);
        }

        public void SetName(string name)
        {
            _name.text = name;
        }

        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void SetIncome(int income)
        {
            _income.text = $"Income: {income} / sec";
        }

        public void SetLevel(int level, int maxLevel)
        {
            _level.text = $"Level: {level}/{maxLevel}";
        }

        public void SetPopulation(int population)
        {
            _population.text = $"Population: {population}";
        }

        public void SetPrice(int price)
        {
            _upgradePrice.text = price.ToString();
        }

        public void SetIcon(Sprite icon)
        {
            _icon.sprite = icon;
        }

        public void EnableUpgradeButton(bool value)
        {
            _upgradeButton.interactable = value;
            _pricePanel.SetActive(value);
        }

        private void UpgradePlanet()
        {
            UpdateButtonClicked?.Invoke();
        }

        private void ClosePopup()
        {
            PopupClosed?.Invoke();
            gameObject.SetActive(false);
        }
    }
}