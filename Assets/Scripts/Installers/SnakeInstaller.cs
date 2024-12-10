using Core;
using Modules;
using PlayerInput;
using Player;
using SnakeGame;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class SnakeInstaller : MonoInstaller
    {
        private const int MaxLevel = 9;

        [SerializeField] private Snake _snake;
        [SerializeField] private Coin _coinPrefab;
        [SerializeField] private WorldBounds _worldBounds;
        [SerializeField] private Transform _coinsParent;

        public override void InstallBindings()
        {
            //Snake
            Container.BindInterfacesTo<Snake>()
                .FromInstance(_snake)
                .AsSingle();

            Container.BindInterfacesTo<MoveInput>()
                .AsSingle();

            Container.BindInterfacesTo<SnakeMoveController>()
                .AsSingle()
                .NonLazy();

            //Components
            Container.BindInterfacesTo<Difficulty>()
                .AsSingle()
                .WithArguments(MaxLevel);

            Container.BindInterfacesTo<WorldBounds>()
                .FromInstance(_worldBounds)
                .AsSingle();

            Container.Bind<Coin>()
                .FromInstance(_coinPrefab)
                .AsCached();

            //Pool
            Container.BindMemoryPool<Coin, CoinPool>()
                .ExpandByOneAtATime()
                .FromComponentInNewPrefab(_coinPrefab)
                .WithGameObjectName("Coin")
                .UnderTransform(_coinsParent)
                .AsSingle();

            //Core
            Container.BindInterfacesAndSelfTo<CoinSpawner>()
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesTo<GameManager>()
                .AsSingle()
                .NonLazy();
        }
    }
}