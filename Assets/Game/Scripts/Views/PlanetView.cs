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
        [SerializeField] private GameObject _incomePanel;
        [SerializeField] private GameObject _pricePanel;
        
        [Header("Icons")]
        [SerializeField] private GameObject _coinIcon;
        [SerializeField] private GameObject _lockIcon;

        [Header("Text")]
        [SerializeField] private TMP_Text _price;
        [SerializeField] private TMP_Text _time;
        
        [Header("Bars")]
        [SerializeField] private Image _progressBar;

        public Vector3 CoinPosition => _coinIcon.transform.position;
        
        private void Start()
        {
            _coinIcon.SetActive(false);
        }

        public void Initialize(bool isUnlocked)
        {
            _incomePanel.SetActive(isUnlocked);
            _lockIcon.SetActive(!isUnlocked);
            _pricePanel.SetActive(!isUnlocked);
        }

        public void SetPrice(int price)
        {
            _price.text = price.ToString();
        }

        public void SetProgress(float progress, float time)
        {
            _progressBar.fillAmount = progress;
            _time.text = $"{(int)(time / 60f)}m:{(int)(time % 60f)}s";
        }
    }
}