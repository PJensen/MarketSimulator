using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MarketSimulator.Core.Indicators
{
    /// <summary>
    /// Technical1
    /// </summary>
    [DebuggerDisplay("{Name}")]
    public abstract class Technical : IEquatable<string>, IEquatable<Technical>
    {
        #region Technical Categories

        protected const string OverlayCategory = "Overlays";
        protected const string BreadthIndicators = "Breadth Indicators";
        protected const string PriceBased = "Price-based indicators";
        protected const string VolumeBased = "Volume-based indicators";

        #endregion

        /// <summary>
        /// Technical
        /// </summary>
        /// <param name="name"></param>
        protected Technical(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Technical
        /// </summary>
        /// <param name="name">the name of this technical indicator</param>
        /// <param name="description">the description of this technical indicator</param>
        protected Technical(string name, string description)
        {
            Name = name;
            Description = description;
        }

        /// <summary>
        /// MarketTick
        /// </summary>
        /// <returns></returns>
        public abstract void MarketTick(MarketTickEventArgs mktTickEventArgs);

        /// <summary>
        /// Clear all technical data
        /// </summary>
        public abstract void Clear();

        /// <summary>
        /// The name of this techincal indicator
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// The description of this technical indicator
        /// </summary>
        public string Description { get; protected set; }

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

        #region Implementation of IEquatable<Technical>

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(Technical other)
        {
            return Name.Equals(other.Name);
        }

        #endregion
    }
}
