using System.Data;

namespace MarketSimulator.Interfaces
{
    /// <summary>
    /// IMarketDataRetriever
    /// </summary>
    public interface IMarketDataRetriever
    {
        /// <summary>
        /// Retrieve
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        DataTable Retrieve(string symbol, out string message, out bool fail);
    }
}
