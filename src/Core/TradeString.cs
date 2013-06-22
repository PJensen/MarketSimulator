using MarketSimulator.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketSimulator.Core
{
    public class TradeString
    {
        public TradeString()
        {
            BuyLine = new List<BuyEventArgs>();
            SellLine = new List<SellEventArgs>();
        }

        /// <summary>
        /// TotalSharesAfterExecution
        /// </summary>
        public int TotalSharesAfterExecution
        {
            get
            {
                return TotalPurchasedShares - TotalSoldShares;
            }
        }

        /// <summary>
        /// TotalPurchasedShares
        /// </summary>
        public int TotalPurchasedShares
        {
            get { return BuyLine.Sum(f => f.Shares); }
        }

        public double TotalPurchasedMV
        {
            get { return BuyLine.Sum(f => f.Shares * f.MarketData.Close); }
        }

        /// <summary>
        /// TotalPurchasedShares
        /// </summary>
        public int TotalSoldShares
        {
            get { return SellLine.Sum(f => f.Shares); }
        }

        public double TotalSoldMV
        {
            get { return SellLine.Sum(f => f.Shares * f.MarketData.Close); }
        }

        public List<BuyEventArgs> BuyLine { get; set; }
        public List<SellEventArgs> SellLine { get; set; }


        public void Clear()
        {
            BuyLine.Clear();
            SellLine.Clear();
        }

        /// <summary>
        /// IsValid
        /// </summary>
        public bool IsValid
        {
            get
            {
                return BuyLine.Count > 0 && SellLine.Count > 0;
            }
        }
    }
}
