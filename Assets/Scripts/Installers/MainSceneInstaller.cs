using Modules;
using SnakeGame;
using UI;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class MainSceneInstaller : MonoInstaller
    {
        private const int MaxLevel = 3;

        [SerializeField] private Snake _snake;
        [SerializeField] private WorldBounds _worldBounds;

        [Header("Coins")]
        [SerializeField] private Coin _coinPrefab;
        [SerializeField] private Transform _coinsParent;

        [Header("UI Elements")]
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private LevelView _levelView;
        [SerializeField] private GameObject _winPopup;
        [SerializeField] private GameObject _losePopup;

        public override void InstallBindings()
        {
            //Snake
            SnakeInstaller.Install(Container, _snake);

            //Modules
            Container.BindInterfacesTo<Difficulty>()
                .AsSingle()
                .WithArguments(MaxLevel);

            Container.BindInterfacesTo<WorldBounds>()
                .FromInstance(_worldBounds)
                .AsSingle();

            Container.BindInterfacesTo<Score>()
                .AsSingle();

            //Coin
            CoinInstaller.Install(Container, _coinPrefab, _coinsParent);

            //UI
            UIInstaller.Install(Container, _scoreView, _levelView);

            //Core
            SystemInstaller.Install(Container, _winPopup, _losePopup);
        }
    }
}