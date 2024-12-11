using UseCases.Coins;
using Modules;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class CoinsInstaller : Installer<Coin, Transform, CoinsInstaller>
    {
        private const string CoinName = "Coin";

        private readonly Coin _coinPrefab;
        private readonly Transform _coinsParent;

        public CoinsInstaller(Coin coinPrefab, Transform coinsParent)
        {
            _coinPrefab = coinPrefab;
            _coinsParent = coinsParent;
        }

        public override void InstallBindings()
        {
            Container.BindMemoryPool<Coin, CoinPool>()
                .ExpandByOneAtATime()
                .FromComponentInNewPrefab(_coinPrefab)
                .WithGameObjectName(CoinName)
                .UnderTransform(_coinsParent)
                .AsSingle();

            Container.BindInterfacesAndSelfTo<CoinSpawner>()
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesTo<CoinsCollector>()
                .AsSingle()
                .NonLazy();
        }
    }
}