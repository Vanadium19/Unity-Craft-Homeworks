using Modules;
using SnakeGame;
using UI;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class MainSceneInstaller : MonoInstaller
    {
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
            ModulesInstaller.Install(Container,_worldBounds);

            //Coin
            CoinsInstaller.Install(Container, _coinPrefab, _coinsParent);

            //System
            SystemInstaller.Install(Container);
            
            //UI
            UIInstaller.Install(Container, _scoreView, _levelView);
            
            //Presenters
            PresentersInstaller.Install(Container, _winPopup, _losePopup);
        }
    }
}