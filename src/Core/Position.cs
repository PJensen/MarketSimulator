using MarketSimulator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketSimulator.Core
{
    /// <summary>
    /// Position
    /// </summary>
    public struct Position : IPosition
    {
        /// <summary>
        /// Position; positons are transient
        /// </summary>
        /// <param name="symbol">the symbol</param>
        /// <param name="shares">the number of shares</param>
        /// <param name="price">the price</param>
        public Position(string symbol, int shares, double price)
        {
            this.symbol = symbol;
            this.shares = shares;
            this.price = price;
        }

        #region Public facing properties

        /// <summary>
        /// Symbol
        /// </summary>
        public string Symbol { get { return symbol; } }

        /// <summary>
        /// Price
        /// </summary>
        public double Price { get { return price; } }

        /// <summary>
        /// Shares
        /// </summary>
        public int Shares { get { return shares; } }

        #endregion

        #region Fields
        readonly string symbol;
        readonly int shares;
        readonly double price;
        #endregion
    }
}
