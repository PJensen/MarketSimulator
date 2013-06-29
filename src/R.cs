using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using MarketSimulator.Core;
using System.Windows.Forms;

namespace MarketSimulator
{
    /// <summary>
    /// Runtime
    /// </summary>
    public static class R
    {
        /// <summary>
        /// random
        /// </summary>
        private static readonly Random random = new Random((int)DateTime.Now.Ticks);

        /// <summary>
        /// Save action
        /// </summary>
        private static readonly Action Save = Properties.Settings.Default.Save;

        /// <summary>
        /// ExitConfirmation
        /// </summary>
        public static bool ExitConfirmation
        {
            get { return Properties.Settings.Default.ExitConfirmation; }
            set
            {
                Properties.Settings.Default.ExitConfirmation = value;
                Save();
            }
        }

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
        /// EstimatedTicks; useful for building large lists with predefined capacity.
        /// </summary>
        public static int EstimatedTicks
        {
            get { return Properties.Settings.Default.EstimatedTicks; }
            set { Properties.Settings.Default.EstimatedTicks = value; Save(); }
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
        /// GUI Utility
        /// </summary>
        public static class GUI
        {
            /// <summary>
            /// ScrollDataGridForward
            /// </summary>
            /// <param name="dataGridViewPositions"></param>
            public static void ScrollDataGridForward(DataGridView dataGridViewPositions)
            {
                dataGridViewPositions.FirstDisplayedScrollingRowIndex = dataGridViewPositions.Rows.Count - 1;
            }
        }

        /// <summary>
        /// PRNG
        /// </summary>
        public static Random Random { get { return random; } }

        /// <summary>
        /// Convert
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static List<MarketData> Convert(DataTable table)
        {
            var retVal = new MarketData[table.Rows.Count];

            if (table == null)
                return retVal.ToList();

            for (var i = 0; i < table.Rows.Count; ++i)
            {
                retVal[i] = new MarketData()
                {
                    Date = DateTime.Parse(table.Rows[i]["Date"].ToString()),
                    Open = double.Parse(table.Rows[i]["Open"].ToString()),
                    High = double.Parse(table.Rows[i]["High"].ToString()),
                    Low = double.Parse(table.Rows[i]["Low"].ToString()),
                    Close = double.Parse(table.Rows[i]["Close"].ToString()),
                    Volume = long.Parse(table.Rows[i]["Volume"].ToString())
                };
            }

            retVal = retVal.Reverse().ToArray();

            for (var i = 1; i < table.Rows.Count; ++i)
            {
                retVal[i].Prev = retVal[i - 1];
            }

            for (var i = 0; i < table.Rows.Count - 1; ++i)
            {
                retVal[i].Next = retVal[i + 1];
            }

            return retVal.ToList();
        }
    }
}
