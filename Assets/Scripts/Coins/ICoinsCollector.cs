using System;
using Modules;

namespace Coins
{
    public interface ICoinsCollector
    {
        event Action<ICoin> CoinCollected;
        event Action AllCoinCollected;
    }
}