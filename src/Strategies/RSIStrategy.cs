using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
using MarketSimulator.Forms;

namespace MarketSimulator.Strategies
{
    /// <summary>
    /// 
    /// </summary>
    public class RSIStrategy : StrategyBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataManipulator"></param>
        public RSIStrategy()
            : base()
        {
        }

        public int holdingPeriod { get; set; }

        public Boolean bought { get; set; }
        public Boolean waitingToBuy { get; set; }
        public double purchasePrice { get; set; }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void MarketTick(object sender, MarketTickEventArgs e)
        {
            
            MarketSimulator.Instance.Balance = MarketSimulator.Instance.cash + (e.marketData.Close) * MarketSimulator.Instance.Shares;
            if (BuySignal(e.RSI))
            {
                waitingToBuy = true;
            }

            if (waitingToBuy && e.RSI > 40 && ((MarketSimulator.Instance.Shares * e.marketData.Close) / MarketSimulator.Instance.Balance < 0.7))
            {
                //var shares = (int)(MarketSimulator.Instance.Balance / 10 / e.marketData.Close);
                var shares = (int)((MarketSimulator.Instance.Balance / 10) / e.marketData.Close);
                MarketSimulator.Instance.Shares += shares;
                MarketSimulator.Instance.cash = MarketSimulator.Instance.Balance - MarketSimulator.Instance.Shares * e.marketData.Close;
                purchasePrice = e.marketData.Close;
                bought = true;
                OnBuyEvent(e);
            }
            if (bought)
            {
                holdingPeriod++;
            }
            //if (holdingPeriod > 30)
            if (holdingPeriod > 120)
            {
                holdingPeriod = 0;

                MarketSimulator.Instance.cash +=
                    MarketSimulator.Instance.Shares * e.marketData.Close;
                MarketSimulator.Instance.Balance = MarketSimulator.Instance.cash;
                bought = false;
                waitingToBuy = false;
                OnSellEvent(e);
                MarketSimulator.Instance.Shares = 0;
            }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        private bool BuySignal(double rsi)
        {
            return rsi < 30;
        }
    }
}
