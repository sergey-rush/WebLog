namespace WebLog.Logger.Metrics.Config
{
    /// <inheritdoc cref="IMetricOptions" />
    public class MetricOptions : IMetricOptions
    {
        /// <summary>
        /// Наименование метрики.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание метрики.
        /// </summary>
        public string Help { get; set; }

        /// <summary>
        /// Label names
        /// </summary>
        public string[] LabelNames { get; set; }
        
        /// <summary>
        /// Label values
        /// </summary>
        public string[] LabelValues { get; set; }

        /// <summary>
        /// Array of values for histogram
        /// </summary>
        public double[] Buckets { get; set; }

        /// <summary>
        /// Value to observe
        /// </summary>
        public double ObserveValue { get; set; }
    }
}