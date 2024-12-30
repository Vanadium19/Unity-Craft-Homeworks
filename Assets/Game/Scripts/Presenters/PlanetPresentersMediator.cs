using UnityEngine;

namespace Game.Presenters
{
    public class PlanetPresentersMediator : IPlanetPresentersMediator
    {
        private readonly MoneyPresenter _moneyPresenter;

        public PlanetPresentersMediator(MoneyPresenter moneyPresenter)
        {
            _moneyPresenter = moneyPresenter;
        }

        public void GatherIncome(Vector3 planetPosition, int range)
        {
            _moneyPresenter.AddMoney(planetPosition, range);
        }
    }
}