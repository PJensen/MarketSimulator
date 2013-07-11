using MarketSimulator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketSimulator.Strategies
{
    public class MomentumStrategy : StrategyBase
    {
        public MomentumStrategy()
            : base("Momentum")
        { }
        public class Consecutive
        {
            /// <summary>
            /// Consecutive
            /// </summary>
            /// <param name="security"></param>
            public Consecutive(string security)
            {
                Security = security;
                MarketData = default(MarketData);
            }

            /// <summary>
            /// The security
            /// </summary>
            public string Security { get; private set; }

            /// <summary>
            /// UpDays
            /// </summary>
            public int UpDays { get; private set; }

            /// <summary>
            /// DownDays
            /// </summary>
            public int DownDays { get; private set; }

            /// <summary>
            /// The last market data to have ticked
            /// </summary>
            public MarketData MarketData { get; private set; }

            /// <summary>
            /// StreakList
            /// </summary>
            List<int> StreakList = new List<int>();

            /// <summary>
            /// The streak average
            /// </summary>
            public double StreakAverage
            {
                get
                {
                    return StreakList.Count <= 0 ? 0.0d : StreakList.Average();
                }
            }

            /// <summary>
            /// Streak
            /// </summary>
            public int Streak
            {
                get { return new[] { UpDays, DownDays }.Max(); }
            }

            /// <summary>
            /// Reset
            /// </summary>
            public void Reset()
            {
                UpDays = 0;
                DownDays = 0;
            }

            /// <summary>
            /// Process
            /// </summary>
            /// <param name="e"></param>
            public void Process(MarketTickEventArgs e)
            {
                if (!e.Symbol.Equals(Security))
                {
                    return;
                }

                if (MarketData == null)
                {
                    MarketData = e.MarketData;

                    return;
                }

                if (e.MarketData.Close >= MarketData.Close)
                {
                    if (DownDays > 0)
                    {
                        Reset();
                    }

                    UpDays++;
                }
                else
                {
                    if (UpDays > 0)
                    {
                        Reset();
                    }

                    DownDays++;
                }

                StreakList.Add(Streak);

                MarketData = e.MarketData;
            }
        }


        /// <summary>
        /// ConsecutiveDays
        /// </summary>
        Dictionary<string, Consecutive> ConsecutiveDays = new Dictionary<string, Consecutive>();

        /// <summary>
        /// this
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Consecutive this[string index]
        {
            get
            {
                if (ConsecutiveDays.ContainsKey(index))
                {
                    return ConsecutiveDays[index];
                }

                ConsecutiveDays.Add(index, new Consecutive(index));

                return this[index];
            }
        }

        public override Events.StrategyMarketTickResult MarketTick(object sender, MarketTickEventArgs e)
        {
            string symbol = e.Symbol;
            this[symbol].Process(e);

            return base.MarketTick(sender, e);
        }

        public override Events.BuyEventArgs BuySignal(MarketTickEventArgs eventArgs)
        {
            int maxShares = (int)(eventArgs.StrategyInfo.Cash / eventArgs.MarketData.Close);

            string symbol = eventArgs.Symbol;

            if (this[symbol].UpDays > this[symbol].StreakAverage)
            {
                if (maxShares > this[symbol].Streak)
                {
                    return Buy(this[symbol].Streak);
                }
            }

            return null;
        }

        public override Events.SellEventArgs SellSignal(MarketTickEventArgs eventArgs)
        {           
            string symbol = eventArgs.Symbol;
            int maxSharesBuy = (int)(eventArgs.StrategyInfo.Cash / eventArgs.MarketData.Close);
            int maxSharesSell = eventArgs.StrategyInfo.PositionData.SecurityShares(eventArgs.MarketData.Date, symbol);

            if (this[symbol].DownDays > this[symbol].StreakAverage)
            {
                return Sell(this[symbol].Streak);
            }

            return null;
        }
    }
}
