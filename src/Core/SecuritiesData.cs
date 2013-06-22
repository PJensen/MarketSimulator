using MarketSimulator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketSimulator.Core
{
    /// <summary>
    /// GlobalSecuritiesData
    /// </summary>
    public class GlobalSecuritiesData : Dictionary<string, List<MarketData>>
    {
        /// <summary>
        /// GlobalSecuritiesData
        /// </summary>
        public GlobalSecuritiesData()
            : base()
        { }

        /// <summary>
        /// GlobalSecuritiesData
        /// </summary>
        public GlobalSecuritiesData(int capacity = 1024)
            : base(capacity)
        { }

        /// <summary>
        /// CanTickPast; here to be self documenting
        /// TODO: Examine for off by 1 error; just in case
        /// </summary>
        /// <param name="queryTick"></param>
        /// <returns></returns>
        public bool CanSecurityTickPast(string security, int n)
        {
            return this[security].Count > n;
        }
    }

    /// <summary>
    /// SecuritiesData
    /// </summary>
    public class SecuritiesSnap : Dictionary<string, MarketData>
    {
        /// <summary>
        /// SecuritiesData
        /// </summary>
        public SecuritiesSnap()
            : base()
        { }

        /// <summary>
        /// Date
        /// </summary>
        public DateTime Date
        {
            // Note: All dates should be the same just grab the first one
            // or return NULL.
            get { return Values.FirstOrDefault().Date; }
        }

        /// <summary>
        /// Close
        /// </summary>
        public double Close
        {
            get { return Values.Average(f => f.Close); }
        }

        /// <summary>
        /// Open
        /// </summary>
        public double Open
        {
            get { return Values.Average(f => f.Open); }
        }

        /// <summary>
        /// High
        /// </summary>
        public double High
        {
            get { return Values.Max(f => f.High); }
        }

        /// <summary>
        /// Low
        /// </summary>
        public double Low
        {
            get { return Values.Min(f => f.Low); }
        }

        /// <summary>
        /// Volume
        /// </summary>
        public long Volume
        {
            get { return Values.Sum(f => f.Volume); }
        }

        /// <summary>
        /// PriceTotal interesting for homemade index stuff
        /// </summary>
        public double PriceTotal
        {
            get { return Values.Sum(f => f.Close); }
        }
    }
}
