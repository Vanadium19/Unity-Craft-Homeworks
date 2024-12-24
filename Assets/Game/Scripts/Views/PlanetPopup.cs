using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Game.Views
{
    public class PlanetPopup : MonoBehaviour
    {
        [Header("Button")]
        [SerializeField] private Image _icon;
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private Button _closeButton;

        [Header("Info")]
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _population;
        [SerializeField] private TMP_Text _level;
        [SerializeField] private TMP_Text _income;
        [SerializeField] private TMP_Text _upgradePrice;

        public event Action UpdateButtonClicked;

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

        public void ShowInfo(int population, int level, int income)
        {
            _population.text = population.ToString();
            _level.text = level.ToString();
            _income.text = income.ToString();
        }

        public void SetPrice(int price)
        {
            _upgradePrice.text = price.ToString();
        }

        public void SetIcon(Sprite icon)
        {
            _icon.sprite = icon;
        }

        private void UpgradePlanet()
        {
            UpdateButtonClicked?.Invoke();
        }

        private void ClosePopup()
        {
            gameObject.SetActive(false);
        }
    }
}