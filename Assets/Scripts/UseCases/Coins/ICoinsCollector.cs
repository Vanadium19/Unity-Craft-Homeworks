using System;

namespace UseCases.Coins
{
    public interface ICoinsCollector
    {
        event Action AllCoinCollected;
    }
}