namespace MarketSimulator.Core.Indicators
{
    /// <summary>
    /// ITechnicalValue
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITechnicalValue<out T>
    {
        /// <summary>
        /// The value of this technical
        /// </summary>
        T Value { get; }
    }
}