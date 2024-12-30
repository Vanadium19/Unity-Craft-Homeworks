using System;
using Modules.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Views
{
    public class PlanetView : MonoBehaviour
    {
        private const float MaxProgress = 1;

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

        private SmartButton _button;

        public event Action OnClicked;
        public event Action OnHold;
        
        public Vector3 CoinPosition => _coinIcon.transform.position;

        private void Awake()
        {
            _button = GetComponentInChildren<SmartButton>();
        }

        private void OnEnable()
        {
            _button.OnClick += OnButtonClicked;
            _button.OnHold += OnButtonHold;
        }

        private void Start()
        {
            _coinIcon.SetActive(false);
        }

        private void OnDisable()
        {
            _button.OnClick -= OnButtonClicked;
            _button.OnHold -= OnButtonHold;
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

        public void SetProgress(float progress, string time)
        {
            _progressBar.fillAmount = progress;
            _time.text = time;

            FinishProgress(progress >= MaxProgress);
        }

        private void FinishProgress(bool value)
        {
            _coinIcon.SetActive(value);
            _incomePanel.SetActive(!value);
        }

        private void OnButtonClicked()
        {
            OnClicked?.Invoke();
        }

        private void OnButtonHold()
        {
            OnHold?.Invoke();
        }
    }
}