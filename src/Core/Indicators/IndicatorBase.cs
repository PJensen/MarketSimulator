using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketSimulator.Core.Indicators
{
    public abstract class Technical: IEquatable<string>
    {
        protected Technical(string name)
        {
            Name = name;
        }

        /// <summary>
        /// MarketTick
        /// </summary>
        /// <returns></returns>
        public abstract void MarketTick(MarketTickEventArgs mktTickEventArgs);

        /// <summary>
        /// The name of this techincal indicator
        /// </summary>
        public string Name { get; set; }

        #region Implementation of IEquatable<string>

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(string other)
        {
            return Name.Equals(other);
        }

        #endregion
    }
}
