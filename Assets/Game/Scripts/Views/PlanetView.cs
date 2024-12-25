using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Game.Views
{
    public class PlanetView : MonoBehaviour
    {
        [Header("Panels")]
        [SerializeField] private GameObject _progressBar;
        [SerializeField] private GameObject _pricePanel;
        
        [Header("Icons")]
        [SerializeField] private GameObject _coinIcon;
        [SerializeField] private GameObject _lockIcon;

        [Header("Text")]
        [SerializeField] private TMP_Text _price;

        private void Start()
        {
            _coinIcon.SetActive(false);
        }

        public void Initialize(bool isUnlocked)
        {
            _progressBar.SetActive(isUnlocked);
            _lockIcon.SetActive(!isUnlocked);
            _pricePanel.SetActive(!isUnlocked);
        }

        public void SetPrice(int price)
        {
            _price.text = price.ToString();
        }
    }
}