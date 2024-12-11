using Modules;
using SnakeGame;
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
        [SerializeField] private GameUI _gameUI;

        public override void InstallBindings()
        {
            //Snake
            SnakeInstaller.Install(Container, _snake);

            //Modules
            ModulesInstaller.Install(Container,_worldBounds);

            //Coin
            CoinsInstaller.Install(Container, _coinPrefab, _coinsParent);

            //UI
            UIInstaller.Install(Container, _gameUI);
            
            //Presenters
            PresentersInstaller.Install(Container);
            
            //System
            SystemInstaller.Install(Container);
        }
    }
}