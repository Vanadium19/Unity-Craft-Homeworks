using Game.Views;
using Modules.Planets;

namespace Game.Presenters
{
    public class PlanetPresenter
    {
        private readonly IPlanet _planet;
        private readonly PlanetView _planetView;
        private readonly PopupShower _popupShower;
        private readonly PlanetPresentersMediator _mediator;

        public PlanetPresenter(IPlanet planet,
            PlanetView planetView,
            PopupShower popupShower,
            PlanetPresentersMediator mediator)
        {
            _planet = planet;
            _planetView = planetView;
            _popupShower = popupShower;
            _mediator = mediator;
        }

        public void Initialize()
        {
            _planetView.OnHold += OnHold;
            _planet.OnGathered += OnGathered;
            _planetView.OnClicked += OnClick;
            _planet.OnIncomeTimeChanged += ChangeIncomeTime;

            _planetView.Initialize(_planet.IsUnlocked);
            _planetView.SetPrice(_planet.Price);

            if (!_planet.IsUnlocked)
                _planet.OnUnlocked += UnlockPlanet;
        }

        public void Dispose()
        {
            _planetView.OnHold -= OnHold;
            _planet.OnGathered -= OnGathered;
            _planetView.OnClicked -= OnClick;
            _planet.OnIncomeTimeChanged -= ChangeIncomeTime;
        }

        private void GatherIncome()
        {
            _planet.GatherIncome();
        }

        private void OnHold()
        {
            _popupShower.OpenPopup(_planet);
        }

        private void OnGathered(int range)
        {
            _mediator.GatherIncome(_planetView.CoinPosition, range);
        }

        private void OnClick()
        {
            if (!_planet.IsUnlocked)
                _planet.Unlock();

            if (_planet.IsIncomeReady)
                GatherIncome();
        }

        private void UnlockPlanet()
        {
            _planetView.Initialize(_planet.IsUnlocked);

            _planet.OnUnlocked -= UnlockPlanet;
        }

        private void ChangeIncomeTime(float time)
        {
            string timeText = $"{(int)(time / 60f)}m:{(int)(time % 60f)}s";

            _planetView.SetProgress(_planet.IncomeProgress, timeText);
        }
    }
}