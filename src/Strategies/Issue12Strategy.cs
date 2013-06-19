using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketSimulator.Strategies
{
    public class Issue12Strategy : StrategyBase
    {
        public Issue12Strategy()
            : base("Issue 12 Strategy") { }

        public override Events.BuyEventArgs BuySignal(MarketTickEventArgs e)
        {
            if (e.RSI < 20)
            {
                return new Events.BuyEventArgs(e.MarketData, 10, e.MarketData.Low, e.MarketData.High, Core.TradeFlags.StopLimit);
            }

            return null;
        }

        public override Events.SellEventArgs SellSignal(MarketTickEventArgs e)
        {
            // never sell
            return null;
        }
    }
}
