using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MarketSimulator.Core;
using MarketSimulator.Events;
using MarketSimulator.Strategies;

namespace MarketSimulator
{
    /// <summary>
    /// MarketSimulator
    /// </summary>
    public class MarketSimulator
    {
        #region Singleton

        /// <summary>
        /// _instance
        /// </summary>
        private static volatile MarketSimulator _instance;

        /// <summary>
        /// syncRoot
        /// </summary>
        private static readonly object syncRoot = new Object();

        /// <summary>
        /// MarketSimulator
        /// </summary>
        private MarketSimulator()
        {
            Balance = Cash = Properties.Settings.Default.StartingBalance;
            MarketData = new List<MarketData>();
            CurrentStrategy = default(StrategyBase);
        }

        /// <summary>
        /// Instance
        /// </summary>
        public static MarketSimulator Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (syncRoot)
                    {
                        if (_instance == null)
                            _instance = new MarketSimulator();
                    }
                }

                return _instance;
            }
        }

        #endregion

        public int Shares { get; set; }
        public double Balance { get; set; }
        public int Tick { get; set; }
        public int NumberOfTrades { get; set; }

        public double PXLast { get; set; }

        public double PaperValue
        {
            get { return PXLast * Shares; }
        }

        public double Cash { get; set; }

        public void OnTickEvent(MarketTickEventArgs eventArgs)
        {
            
            PXLast = eventArgs.marketData.Close;
            Instance.Balance = Instance.Cash; // +(eventArgs.marketData.Close) * Instance.Shares;
            Tick++;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventArgs"></param>
        public void OnBuyEvent(BuyEventArgs eventArgs)
        {
            var totalValue = eventArgs.Shares * eventArgs.MarketData.Close;

            if (totalValue >= Instance.Cash)
            {
                eventArgs.Cancel = true;

                return;
            }

            Instance.Cash -= totalValue;
            Instance.Shares += eventArgs.Shares;
            NumberOfTrades++;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventArgs"></param>
        public void OnSellEvent(SellEventArgs eventArgs)
        {
            if (eventArgs.Shares <= 0)
            {
                eventArgs.Cancel = true;
                return;
            }

            if (eventArgs.Shares > Instance.Shares)
                eventArgs.Shares = Instance.Shares;

            Instance.Shares -= eventArgs.Shares;
            Instance.Cash += eventArgs.Shares * eventArgs.MarketData.Close;
            NumberOfTrades++;
        }

        /// <summary>
        /// CurrentStrategy
        /// </summary>
        public StrategyBase CurrentStrategy { get; set; }

        /// <summary>
        /// MarketData
        /// </summary>
        public List<MarketData> MarketData { get; set; }
    }
}
