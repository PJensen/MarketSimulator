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
        /// <param name="shares">shares quantity</param>
        public TradeEventArgs(TradeType tradeType, IPosition position, MarketData marketData)
        {
            MarketData = marketData;
            Position = position;
            TradeType = tradeType;
        }

        /// <summary>
        /// Shares
        /// </summary>
        public int Shares { get { return Position.Shares; } }

        /// <summary>
        /// Symbol
        /// </summary>
        public string Symbol { get { return Position.Symbol; } }

        /// <summary>
        /// Price
        /// </summary>
        public double Price { get { return Position.Price; } }

        /// <summary>
        /// MarketData
        /// </summary>
        public MarketData MarketData { get; set; }

        /// <summary>
        /// Position
        /// </summary>
        public IPosition Position { get; set; }

        /// <summary>
        /// TradeType
        /// </summary>
        public TradeType TradeType { get; protected set; }

        /// <summary>
        /// Cancel
        /// </summary>
        public bool Cancel { get; set; }
    }
}
