using System;
using WebLog.Logger.Metrics.Config;
using Prometheus;

namespace WebLog.Logger.Metrics
{
     /// <summary>
    /// Updates metrics for all the registered collectors and sends them to the specified exporter.
    /// </summary>
    public class Metric:IMetric
    {
        /// <summary>
        /// Increments counter
        /// </summary>
        /// <param name="configureOptions"></param>
        public void IncrementCounter(Action<IMetricOptions> configureOptions)
        {
            var options = new MetricOptions();
            configureOptions(options);
            
            var counter = Prometheus.Metrics.CreateCounter(options.Name, options.Help, options.LabelNames);
            counter.WithLabels(options.LabelValues).Inc();
        }
        
        /// <summary>
        /// Increments gauge
        /// </summary>
        /// <param name="configureOptions"></param>
        public void IncrementGauge(Action<IMetricOptions> configureOptions)
        {
            var options = new MetricOptions();
            configureOptions(options);
            var gauge = Prometheus.Metrics.CreateGauge(options.Name, options.Help, new GaugeConfiguration
            {                
                LabelNames = options.LabelNames
            });
            gauge.WithLabels(options.LabelValues).Inc();
        }
        
        /// <summary>
        /// Observes histogram
        /// </summary>
        /// <param name="configureOptions"></param>
        public void ObserveHistogram(Action<IMetricOptions> configureOptions)
        {
            var options = new MetricOptions();
            configureOptions(options);
            var histogram = Prometheus.Metrics.CreateHistogram(options.Name, options.Help, new HistogramConfiguration
            {                
                Buckets = options.Buckets
            });
            histogram.Observe(options.ObserveValue);
        }
        
        /// <summary>
        /// Observes summary
        /// </summary>
        /// <param name="configureOptions"></param>
        public void ObserveSummary(Action<IMetricOptions> configureOptions)
        {
            var options = new MetricOptions();
            configureOptions(options);
            var summary = Prometheus.Metrics.CreateSummary(options.Name, options.Help, new SummaryConfiguration
            {                
                LabelNames = options.LabelNames
            });
            summary.Observe(options.ObserveValue);
        }
    }
}