using Game.Views;
using Modules.Planets;
using Zenject;

namespace Game.Presenters
{
    public class PlanetPresenterFactory : PlaceholderFactory<IPlanet, PlanetView, PlanetPresenter> { }
}