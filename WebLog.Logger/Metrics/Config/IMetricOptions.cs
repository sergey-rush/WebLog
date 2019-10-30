namespace WebLog.Logger.Metrics.Config
{
    /// <summary>
    /// Настройки метрики.
    /// </summary>
    public interface IMetricOptions
    {
        /// <summary>
        /// Наименование метрики.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Описание метрики.
        /// </summary>
        string Help { get; set; }

        /// <summary>
        /// Label names
        /// </summary>
        string[] LabelNames { get; set; }
        
        /// <summary>
        /// Label values
        /// </summary>
        string[] LabelValues { get; set; }
        
        /// <summary>
        /// Array of values for histogram
        /// </summary>
        double[] Buckets { get; set; }
        
        /// <summary>
        /// Value to observe
        /// </summary>
        double ObserveValue { get; set; }
    }
}