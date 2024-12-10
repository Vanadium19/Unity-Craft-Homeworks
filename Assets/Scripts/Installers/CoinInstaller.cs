using Coins;
using Modules;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class CoinInstaller : Installer<Coin, Transform, CoinInstaller>
    {
        private const string CoinName = "Coin";

        [Inject] private Coin _coinPrefab;
        [Inject] private Transform _coinsParent;

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