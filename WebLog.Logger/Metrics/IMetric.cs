using System;
using WebLog.Logger.Metrics.Config;

namespace WebLog.Logger.Metrics
{
    /// <summary>
    /// Updates metrics for all the registered collectors and sends them to the specified exporter.
    /// </summary>
    public interface IMetric
    {
        /// <summary>
        /// Increments counter
        /// </summary>
        /// <param name="configureOptions"></param>
        void IncrementCounter(Action<IMetricOptions> configureOptions);

        /// <summary>
        /// Increments gauge
        /// </summary>
        /// <param name="configureOptions"></param>
        void IncrementGauge(Action<IMetricOptions> configureOptions);

        /// <summary>
        /// Observes histogram
        /// </summary>
        /// <param name="configureOptions"></param>
        void ObserveHistogram(Action<IMetricOptions> configureOptions);

        /// <summary>
        /// Observes summary
        /// </summary>
        /// <param name="configureOptions"></param>
        void ObserveSummary(Action<IMetricOptions> configureOptions);

    }
}