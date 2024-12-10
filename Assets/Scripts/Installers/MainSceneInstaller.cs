using Core;
using Modules;
using PlayerInput;
using Player;
using SnakeGame;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class MainSceneInstaller : MonoInstaller
    {
        private const int MaxLevel = 9;

        [SerializeField] private Snake _snake;
        [SerializeField] private Coin _coinPrefab;
        [SerializeField] private WorldBounds _worldBounds;
        [SerializeField] private Transform _coinsParent;

        public override void InstallBindings()
        {
            //Snake
            SnakeInstaller.Install(Container, _snake);

            //Components
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

            //Core
            SystemInstaller.Install(Container);
        }
    }
}