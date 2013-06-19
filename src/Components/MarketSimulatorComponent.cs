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
    public partial class MarketSimulatorComponent : Component
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
        /// StrategyExecutor
        /// </summary>
        public StrategyExecutor StrategyExecutor { get; set; }

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
            return Initialized = StrategyExecutor.LoadMarketData(out message);
        }
    }
}
