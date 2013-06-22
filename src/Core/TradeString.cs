using MarketSimulator.Events;
using MarketSimulator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketSimulator.Core
{
    /// <summary>
    /// TradeString
    /// </summary>
    public class TradeString
    {
        /// <summary>
        /// The security that this trade string relates to
        /// </summary>
        private readonly string security;

        /// <summary>
        /// The security this trade string relates to
        /// </summary>
        public string Security { get { return security; } }

        /// <summary>
        /// Create a new trade string
        /// </summary>
        public TradeString(string security)
        {
            this.security = security;
            BuyLine = new List<IPosition>();
            SellLine = new List<IPosition>();
        }

        /// <summary>
        /// Creates a new trade string using some polymorphic positions
        /// </summary>
        /// <param name="purchases"></param>
        /// <param name="sales"></param>
        public TradeString(string security, IEnumerable<IPosition> purchases, IEnumerable<IPosition> sales)
        {
            this.security = security;
            BuyLine = new List<IPosition>(purchases);
            SellLine = new List<IPosition>(sales);
        }

        /// <summary>
        /// BuyLine
        /// </summary>
        public List<IPosition> BuyLine { get; set; }

        /// <summary>
        /// SellLine
        /// </summary>
        public List<IPosition> SellLine { get; set; }

        /// <summary>
        /// The last purchased price for this trade string
        /// </summary>
        public double LastPurchasedPrice { get { return BuyLine.LastOrDefault().Price; } }

        /// <summary>
        /// The last sold price for this trade string
        /// </summary>
        public double LastSoldPrice { get { return SellLine.LastOrDefault().Price; } }

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

        /// <summary>
        /// Total purchased MV
        /// </summary>
        public double TotalPurchasedMV
        {
            get { return BuyLine.Sum(f => f.Shares * LastPurchasedPrice); }
        }

        /// <summary>
        /// TotalPurchasedShares
        /// </summary>
        public int TotalSoldShares
        {
            get { return SellLine.Sum(f => f.Shares); }
        }

        /// <summary>
        /// Total MV sold in this trade string
        /// </summary>
        public double TotalSoldMV
        {
            get { return SellLine.Sum(f => f.Shares * LastSoldPrice); }
        }



        /// <summary>
        /// Clear buy and sell
        /// </summary>
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
