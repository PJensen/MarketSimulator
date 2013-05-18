﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using MarketSimulator.Core;

namespace MarketSimulator
{
    /// <summary>
    /// Runtime
    /// </summary>
    public static class R
    {
        /// <summary>
        /// Save action
        /// </summary>
        private static readonly Action Save = Properties.Settings.Default.Save;

        /// <summary>
        /// CurrentDirectory
        /// </summary>
        public static string CurrentDirectory
        {
            get { return Directory.GetCurrentDirectory(); }
        }

        /// <summary>
        /// WorkingDirectory
        /// </summary>
        public static string WorkingDirectory
        {
            get { return Properties.Settings.Default.WorkingDirectory; }
            set
            {
                Properties.Settings.Default.WorkingDirectory = value;
                Save();
            }
        }

        /// <summary>
        /// PreviousSecurities
        /// </summary>
        public static StringCollection PreviousSecurities
        {
            get { return Properties.Settings.Default.PreviousSecurities; }
            set
            {
                Properties.Settings.Default.PreviousSecurities = value;
                Save();
            }
        }

        /// <summary>
        /// Convert
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static List<MarketData> Convert(DataTable table)
        {
            var retVal = new List<MarketData>();

            for (var i = 0; i < table.Rows.Count; i++)
            {
                retVal.Add(new MarketData()
                {
                    Date = DateTime.Parse(table.Rows[i]["Date"].ToString()),
                    Open = double.Parse(table.Rows[i]["Open"].ToString()),
                    High = double.Parse(table.Rows[i]["High"].ToString()),
                    Low = double.Parse(table.Rows[i]["Low"].ToString()),
                    Close = double.Parse(table.Rows[i]["Close"].ToString()),
                    Volume = long.Parse(table.Rows[i]["Volume"].ToString())
                });
            }

            return retVal;
        }
    }
}