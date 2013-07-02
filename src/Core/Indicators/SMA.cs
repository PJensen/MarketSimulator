using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketSimulator.Core.Indicators
{
    /// <summary>
    /// SMA
    /// </summary>
    public class SMA : Technical
    {
        private readonly int _period;
        private readonly Queue<double> values;

        /// <summary>
        /// 
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
            : base("SMA")
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
    }
}
