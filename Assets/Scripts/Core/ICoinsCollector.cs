using System;
using Modules;

namespace Core
{
    public interface ICoinsCollector
    {
        event Action AllCoinCollected;
    }
}