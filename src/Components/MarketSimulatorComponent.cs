﻿using MarketSimulator.Core;
using MarketSimulator.Interfaces;
using MarketSimulator.Strategies;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MarketSimulator.Components
{
    /// <summary>
    /// MarketSimulatorComponent
    /// </summary>
    public partial class MarketSimulatorComponent : Component, IStrategyExecutor
    {
        /// <summary>
        /// Creates a new MarketSimulatorComponent
        /// </summary>
        public MarketSimulatorComponent()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Creates a new MarketSimulatorComponent given a container
        /// </summary>
        /// <param name="container">the container to wire up to</param>
        public MarketSimulatorComponent(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }


        /// <summary>
        /// Add a strategy to the strategy executor
        /// </summary>
        /// <param name="strategy">the strategy to add</param>
        public bool AddStrategy(StrategyBase strategy)
        {
            // sanity check to make sure another strategy with the same name is not already there.
            if (Sandboxes.FirstOrDefault(s => s.Strategy.Name == strategy.Name) != null)
                return false;

            Sandboxes.Add(new StrategyExecutionSandbox(this, strategy));

            return true;
        }

        /// <summary>
        /// LoadMarketData
        /// </summary>
        /// <param name="ticker"></param>
        /// <returns></returns>
        public bool LoadMarketData(out string message)
        {
            bool fail;

            MarketData = R.Convert(new YahooDataRetriever().Retrieve(Ticker, out message, out fail));
            MarketData.Reverse();

            return fail;
        }

        /// <summary>
        /// OnTickEvent
        /// </summary>
        /// <param name="eventArgs">market tick event arguments</param>
        public void OnTickEvent(MarketTickEventArgs eventArgs)
        {
            PXLast = eventArgs.MarketData.Close;
            Tick++;
        }

        /// <summary>
        /// MarketTickEvent
        /// </summary>
        //protected event EventHandler<MarketTickEventArgs> MarketTickEvent;

        /// <summary>
        /// The last price that ticked in the simulator
        /// </summary>
        public double PXLast { get; set; }

        /// <summary>
        /// Ticker
        /// </summary>
        public string Ticker { get; private set; }

        /// <summary>
        /// Tick; the same for all strategies being executed
        /// </summary>
        public int Tick { get; private set; }

        /// <summary>
        /// The common backing store for all market data.
        /// </summary>
        public List<MarketData> MarketData { get; set; }

        /// <summary>
        /// Sandboxes
        /// </summary>
        public List<StrategyExecutionSandbox> Sandboxes { get; set; }

        /// <summary>
        /// Has the market simulator component been initialized with a ticker
        /// </summary>
        public bool Initialized { get; private set; }

        /// <summary>
        /// Initializes the market simulator with a given ticker
        /// </summary>
        /// <param name="ticker">the ticker to initialize mkt sim with</param>
        /// <param name="message">the result (string) of the initialization</param>
        /// <returns></returns>
        public bool Initialize(string ticker, out string message)
        {
            return Initialized = LoadMarketData(out message);
        }
    }
}
