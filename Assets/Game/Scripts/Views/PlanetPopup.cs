using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

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

        private IPlanetPopupPresenter _presenter;

        private void OnEnable()
        {
            _upgradeButton.onClick.AddListener(_presenter.UpgradePlanet);
            _closeButton.onClick.AddListener(ClosePopup);

            if (!_presenter.IsPlanetUnlocked)
                _presenter.OnUnlocked += OnUnlocked;

            _presenter.OnUpgrated += SetPlanetInfo;
            _presenter.OnPopulationChanged += OnPopulationChanged;
        }

        private void OnDisable()
        {
            _upgradeButton.onClick.RemoveListener(_presenter.UpgradePlanet);
            _closeButton.onClick.RemoveListener(ClosePopup);

            _presenter.OnUpgrated -= SetPlanetInfo;
            _presenter.OnPopulationChanged -= OnPopulationChanged;
        }

        [Inject]
        public void Construct(IPlanetPopupPresenter presenter)
        {
            _presenter = presenter;
        }

        public void Open()
        {
            gameObject.SetActive(true);

            _name.text = _presenter.Name;
            _icon.sprite = _presenter.Icon;
            SetPlanetInfo();
        }

        private void SetPlanetInfo()
        {
            _level.text = _presenter.Level;
            _income.text = _presenter.Income;
            _upgradePrice.text = _presenter.Price;
            _population.text = _presenter.Population;
            EnableUpgradeButton(_presenter.IsButtonActive);
        }

        private void EnableUpgradeButton(bool value)
        {
            _upgradeButton.interactable = value;
            _pricePanel.SetActive(value);
        }

        private void ClosePopup()
        {
            gameObject.SetActive(false);
            _presenter.OnPopupClosed();
        }

        private void OnUnlocked()
        {
            SetPlanetInfo();
            _icon.sprite = _presenter.Icon;

            _presenter.OnUnlocked -= OnUnlocked;
        }

        private void OnPopulationChanged(string population)
        {
            _population.text = population;
        }
    }
}