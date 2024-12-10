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

            Container.BindInterfacesAndSelfTo<Coin>()
                .FromInstance(_coinPrefab)
                .AsCached();

            Container.BindInterfacesTo<WorldBounds>()
                .FromInstance(_worldBounds)
                .AsSingle();

            //Core
            Container.BindInterfacesTo<CoinSpawner>()
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesTo<GameManager>()
                .AsSingle()
                .NonLazy();
        }
    }
}