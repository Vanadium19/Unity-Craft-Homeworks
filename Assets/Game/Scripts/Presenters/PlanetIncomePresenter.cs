using Game.Views;
using Modules.Planets;

namespace Game.Presenters
{
    public class PlanetIncomePresenter
    {
        private readonly IPlanet _planet;
        private readonly PlanetView _view;

        public PlanetIncomePresenter(IPlanet planet, PlanetView view)
        {
            _planet = planet;
            _view = view;
        }

        public void Initialize()
        {
            _planet.OnIncomeTimeChanged += ChangeIncomeTime;
        }

        public void Dispose()
        {
            _planet.OnIncomeTimeChanged -= ChangeIncomeTime;
        }

        private void ChangeIncomeTime(float time)
        {
            _view.SetProgress(_planet.IncomeProgress, time);
        }
    }
}