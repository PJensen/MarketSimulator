using MarketSimulator.Core;
using MarketSimulator.Events;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public override StrategyMarketTickResult MarketTick(object sender, MarketTickEventArgs e)
        {
            return base.MarketTick(sender, e);
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
