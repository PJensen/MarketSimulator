using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketSimulator.Exceptions
{
    /// <summary>
    /// MarketSimulatorException
    /// </summary>
    public class MarketSimulatorException : Exception
    {
        /// <summary>
        /// MarketSimulatorException
        /// </summary>
        public MarketSimulatorException()
        { }

        /// <summary>
        /// MarketSimulatorException
        /// </summary>
        /// <param name="message">exception message</param>
        public MarketSimulatorException(string message)
            : base(message)
        { }


        /// <summary>
        /// MarketSimulatorException
        /// </summary>
        /// <param name="message">exception message</param>
        /// <param name="innerException">inner exception</param>
        public MarketSimulatorException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
