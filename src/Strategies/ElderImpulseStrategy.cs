using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MarketSimulator.Core;
using MarketSimulator.Events;
using System.Windows.Forms.DataVisualization.Charting;

namespace MarketSimulator.Strategies
{
#if __
    class ElderImpulseStrategy : StrategyBase
    {
        public ElderImpulseStrategy()
            : base("ElderImpulseStrategy", BuyCondition, SellCondition, FinancialFormula.MovingAverageConvergenceDivergence)
        {
        }

        private static int Idx { get; set; }
        private static double previous13EMA { get; set; }
        private static double previousMACD { get; set; }
        private static Boolean bought { get; set; }
       

        private static BuyEventArgs BuyCondition(MarketTickEventArgs eventArgs)
        {
            
            if (Idx % 5 != 0)
                return null;
            

            if (!bought && eventArgs.EMA13 > previous13EMA && eventArgs.MACDHistogram > previousMACD) 
            {
                var shares = (int)(MarketSimulator.Instance.Balance / eventArgs.MarketData.Close);
                bought = true;
                return new BuyEventArgs(eventArgs.MarketData, shares);
            } else 
                return null;
        }

        private static SellEventArgs SellCondition(MarketTickEventArgs eventArgs)
        {
            if (Idx % 5 != 0)
                return null;
            

            if (eventArgs.EMA13 < previous13EMA && eventArgs.MACDHistogram < previousMACD)
            {
                bought = false;
                return new SellEventArgs(eventArgs.MarketData, MarketSimulator.Instance.Shares);
            } 
            else
                return null;
        }
        

        
        public override void MarketTick(object sender, MarketTickEventArgs e)
        {

            base.MarketTick(sender, e);

            Idx++;
            previous13EMA = e.EMA13;
            previousMACD = e.MACDHistogram;

        }
    }
#endif
}
