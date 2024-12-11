using System;
using Modules;

namespace UseCases.Coins
{
    public interface ICoinsCollector
    {
        event Action<ICoin> CoinCollected;
        event Action AllCoinCollected;
    }
}