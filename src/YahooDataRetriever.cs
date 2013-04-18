using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
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
        public List<MarketData> Retrieve(string symbol)
        {
            var tmpDataFile = Path.GetTempFileName();

            using (var webClient = new WebClient())
                webClient.DownloadFile(string.Format(yahooDataURI, symbol), tmpDataFile);

            var table = CSVParser.Parse(File.OpenText(tmpDataFile), true);

            var retVal = new List<MarketData>();

            for (var i = 0; i < table.Rows.Count; i++)
                retVal.Add(new MarketData()
                               {
                                   Date = DateTime.Parse(table.Rows[i]["Date"].ToString()),
                                   Open = double.Parse(table.Rows[i]["Open"].ToString()),
                                   High = double.Parse(table.Rows[i]["High"].ToString()),
                                   Low = double.Parse(table.Rows[i]["Low"].ToString()),
                                   Close = double.Parse(table.Rows[i]["Close"].ToString()),
                                   Volume = long.Parse(table.Rows[i]["Volume"].ToString())
                               });
            return retVal;
        }
    }
}
