using System;

namespace Coins
{
    public interface ICoinsCollector
    {
        event Action AllCoinCollected;
    }
}