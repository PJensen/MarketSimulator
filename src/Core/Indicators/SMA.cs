using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MarketSimulator.Core.Indicators
{
    /// <summary>
    /// SMA50Strategy
    /// </summary>
    [Category(PriceBased)]
    public sealed class SMA50 : SMA, ITechnicalValue<double>
    {
        public SMA50()
            : base(50)
        { }
    }

    /// <summary>
    /// SMA50Strategy
    /// </summary>
    [Category(PriceBased)]
    public sealed class SMA150 : SMA, ITechnicalValue<double>
    {
        public SMA150()
            : base(150)
        { }
    }

    /// <summary>
    /// SMA300
    /// </summary>
    [Category(PriceBased)]
    public sealed class SMA300 : SMA, ITechnicalValue<double>
    {
        public SMA300()
            : base(300)
        { }
    }

    /// <summary>
    /// SMA
    /// </summary>
    [Description("A simple, or arithmetic, moving average that is calculated by adding the closing price of the security for a number of time periods and then dividing this total by the number of time periods.")]
    [Category(PriceBased)]
    public class SMA : Technical, ITechnicalValue<double>
    {
        /// <summary>
        /// The period for this SMA
        /// </summary>
        private readonly int _period;

        /// <summary>
        /// The collection of values for the SMA
        /// </summary>
        private readonly Queue<double> values;

        /// <summary>
        /// historical values for SMA going back to epoch
        /// </summary>
        private readonly Dictionary<DateTime, double> historical;

        /// <summary>
        /// The value of the SMA
        /// </summary>
        public double Value
        {
            get
            {
                return values.Count == _period ? values.Average() : 0;
            }
        }

        /// <summary>
        /// Simple moving average
        /// </summary>
        /// <param name="period"></param>
        public SMA(int period = 50)
            : base(string.Format("SMA {0}", period))
        {
            _period = period;
            values = new Queue<double>(_period);
            historical = new Dictionary<DateTime, double>();
        }

        /// <summary>
        /// Period
        /// </summary>
        public double Period
        {
            get { return _period; }
        }

        #region Overrides of Technical

        /// <summary>
        /// MarketTick
        /// </summary>
        /// <returns></returns>
        public override void MarketTick(MarketTickEventArgs mktTickEventArgs)
        {
            if (historical.ContainsKey(mktTickEventArgs.MarketData.Date))
                return;

            #region FIFO Closing Price Queue of Period Size

            if (values.Count >= _period)
            {
                values.Dequeue();
            }

            values.Enqueue(mktTickEventArgs.MarketData.Close);

            #endregion

            // trap historical value for this tick

            historical.Add(mktTickEventArgs.MarketData.Date, Value);
        }

        #endregion

        /// <summary>
        /// Clear
        /// </summary>
        public override void Clear()
        {
            values.Clear();
            historical.Clear();
        }

        /// <summary>
        /// Historical
        /// </summary>
        public Dictionary<DateTime, double> Historical
        {
            get { return historical; }
        }
    }
}
