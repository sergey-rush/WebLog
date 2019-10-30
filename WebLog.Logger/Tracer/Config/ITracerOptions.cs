using Microsoft.Extensions.Logging;

namespace WebLog.Logger.Tracer.Config
{
    /// <summary>
    /// Определение набора параметров для настройки Jaeger трассировщика.
    /// </summary>
    public interface ITracerOptions
    {
        /// <summary>
        /// Частота выборки.
        /// </summary>
        double SamplingRate { get; set; }

        /// <summary>
        /// Нижний порог выборки.
        /// </summary>
        double LowerBound { get; set; }

        /// <summary>
        /// Экземпляр объекта логирования.
        /// </summary>
        ILoggerFactory LoggerFactory { get; set; }

        /// <summary>
        /// Наименование хоста, используемый агентом.
        /// </summary>
        string JaegerAgentHost { get; set; }

        /// <summary>
        /// Порт хоста, используемый агентом.
        /// </summary>
        int JaegerAgentPort { get; set; }

        /// <summary>
        /// Наименование сервиса.
        /// </summary>
        string ServiceName { get; set; }
    }
}