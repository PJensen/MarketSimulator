using MarketSimulator.Core;
using MarketSimulator.Events;
using MarketSimulator.Strategies;
using System;
namespace MarketSimulator.Interfaces
{
    interface IStrategyEventReciever
    {
        void OnBuyEvent(object sender, BuyEventArgs eventArgs);
        void OnSellEvent(object sender, SellEventArgs eventArgs);
        StrategyBase Strategy { get; }
    }
}
