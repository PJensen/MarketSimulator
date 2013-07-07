using MarketSimulator.Core;
using MarketSimulator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketSimulator.Events
{
    /// <summary>
    /// TradeEventArgs; 
    /// <remarks>some polymorphism here; because a trade event arg may be treated like a position.</remarks>
    /// </summary>
    public class TradeEventArgs : MarketSimulatorEventArgs, IPosition
    {
        /// <summary>
        /// Creates a new TradeEventArgs
        /// </summary>
        /// <param name="tradeType">the trade type</param>
        /// <param name="securitiesData">market data</param>
        /// <param name="Shares">Shares quantity</param>
        public TradeEventArgs(TradeType tradeType, IPosition position, MarketData marketData)
        {
            MarketData = marketData;
            Shares = position.Shares;
            Symbol = position.Symbol;
            Price = position.Price;
            TradeType = tradeType;
        }

        /// <summary>
        /// TradeEventArgs given a market tick and some Shares
        /// </summary>
        /// <param name="e">market tick</param>
        /// <param name="Shares">Shares</param>
        public TradeEventArgs(TradeType tradeType, MarketTickEventArgs e, int shares)
        {
            TradeType = tradeType;
            MarketData = e.MarketData;
            Symbol = e.Symbol;
            Shares = shares;
            Price = e.MarketData.Close;
        }

        /// <summary>
        /// field storage for shares.
        /// </summary>
        private int shares;

        /// <summary>
        /// Shares
        /// </summary>
        public int Shares
        {
            get { return shares; }
            set 
            {
                shares = value; 
            }
        }

        /// <summary>
        /// Symbol
        /// </summary>
        public string Symbol { get; set; }

        /// <summary>
        /// Price
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// MarketTicks
        /// </summary>
        public MarketData MarketData { get; set; }

        /// <summary>
        /// TradeType
        /// </summary>
        public TradeType TradeType { get; protected set; }

        /// <summary>
        /// Cancel
        /// </summary>
        public bool Cancel { get; set; }

        /// <summary>
        /// Date
        /// </summary>
        public DateTime Date
        {
            get { return MarketData.Date; }
            
        }
    }
}
