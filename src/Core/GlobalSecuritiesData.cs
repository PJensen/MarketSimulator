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
}
