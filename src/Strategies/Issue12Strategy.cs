using MarketSimulator.Core;
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

        public override void MarketTick(object sender, MarketTickEventArgs e)
        {
            base.MarketTick(sender, e);
        }

        public override Events.BuyEventArgs BuySignal(MarketTickEventArgs eventArgs)
        {
            return Buy(10);
        }

        public override Events.SellEventArgs SellSignal(MarketTickEventArgs eventArgs)
        {
            return Sell(10);
        }
    }
}
