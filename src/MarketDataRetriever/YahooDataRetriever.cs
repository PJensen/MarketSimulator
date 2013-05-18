using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using MarketSimulator.MarketDataRetriever;
using MarketSimulator.Util;

namespace MarketSimulator
{
    /// <summary>
    /// YahooDataRetriever
    /// </summary>
    public class YahooDataRetriever : IMarketDataRetriever
    {
        /// <summary>
        /// yahooDataURI
        /// </summary>
        private readonly string yahooDataURI = Properties.Settings.Default.YahooURI;

        /// <summary>
        /// Retrieve
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public DataTable Retrieve(string symbol, out string message, out bool fail)
        {
            fail = false;
            message = "Ok";

            var dataFileDest = Path.Combine(R.WorkingDirectory, symbol);

            if (!File.Exists(dataFileDest))
            {
                using (var webClient = new WebClient())
                {
                    try
                    {
                        webClient.DownloadFile(string.Format(yahooDataURI, symbol), dataFileDest);
                    }
                    catch (Exception ex)
                    {
                        message = ex.Message;
                        fail = true;
                    }
                }
            }

            if (fail)
                return default(DataTable);

            return CSVParser.Parse(File.OpenText(dataFileDest), true);
        }
    }
}
