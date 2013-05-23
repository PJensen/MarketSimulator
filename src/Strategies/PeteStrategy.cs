using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MarketSimulator.Core;
using MarketSimulator.Events;
using System.Windows.Forms.DataVisualization.Charting;

namespace MarketSimulator.Strategies
{
    /// <summary>
    /// PeteStrategy
    /// </summary>
    public class PeteStrategy : StrategyBase
    {
        /// <summary>
        /// 
        /// </summary>
        public PeteStrategy()
            : base("PeteStrategy", BuyCondition, SellCondition, FinancialFormula.RelativeStrengthIndex)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventArgs"></param>
        /// <returns></returns>
        private static SellEventArgs SellCondition(MarketTickEventArgs eventArgs)
        {
            if (eventArgs.RSI > 90)
            {
                return new SellEventArgs(eventArgs.MarketData, MarketSimulator.Instance.Shares);
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventArgs"></param>
        /// <returns></returns>
        private static BuyEventArgs BuyCondition(MarketTickEventArgs eventArgs)
        {
            BuyEventArgs buyEventArgs = null;



            if (eventArgs.RSI < 20)
            {
                buyEventArgs = new BuyEventArgs(eventArgs.MarketData, (int)eventArgs.RSI);
            }

            if (buyEventArgs != null && buyEventArgs.Shares == 0)
                return null;

            return buyEventArgs;
        }
    }
}
