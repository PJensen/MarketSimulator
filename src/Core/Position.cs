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
        /// <param name="date"> </param>
        /// <param name="Symbol">the Symbol</param>
        /// <param name="Shares">the number of Shares</param>
        /// <param name="price">the price</param>
        public Position(DateTime date, string symbol, int shares, double price)
            : this()
        {
            Symbol = symbol;
            Shares = shares;
            Price = price;
            Date = date;
        }

        /// <summary>
        /// Position copy constructor
        /// </summary>
        /// <param name="position">position</param>
        public Position(IPosition position)
            : this()
        {
            Symbol = position.Symbol;
            Shares = position.Shares;
            Price = position.Price;
            Date = position.Date;
        }

        #region Public facing properties

        /// <summary>
        /// Date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Symbol
        /// </summary>
        public string Symbol { get; set; }

        /// <summary>
        /// Price
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Shares
        /// </summary>
        public int Shares { get; set; }

        #endregion

        /// <summary>
        /// SameSecurityAndDate
        /// </summary>
        /// <returns>true if the two Positions have the same security and date</returns>
        public bool SameSecurityAndDate(Position other)
        {
             return other.Date.Equals(Date) &&
                other.Symbol.Equals(Symbol);
        }
    }
}
