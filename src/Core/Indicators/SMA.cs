using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MarketSimulator.Core.Indicators
{
    /// <summary>
    /// SMA
    /// </summary>
    [Description("A simple, or arithmetic, moving average that is calculated by adding the closing price of the security for a number of time periods and then dividing this total by the number of time periods.")]
    [Category("")]
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
            if (values.Count >= _period)
            {
                values.Dequeue();
            }

            values.Enqueue(mktTickEventArgs.MarketData.Close);
        }

        #endregion

        /// <summary>
        /// Clear
        /// </summary>
        public override void Clear()
        {
            values.Clear();
        }
    }
}
