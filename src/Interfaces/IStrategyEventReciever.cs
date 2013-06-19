using MarketSimulator.Events;
using MarketSimulator.Strategies;
using System;
namespace MarketSimulator.Interfaces
{
    interface IStrategyEventReciever
    {
        void OnBuyEvent(BuyEventArgs eventArgs);
        void OnSellEvent(SellEventArgs eventArgs);
        StrategyBase Strategy { get; }
    }
}
