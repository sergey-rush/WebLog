using System;
using WebLog.Logger.Metrics.Config;
using WebLog.Logger.Metrics.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Prometheus;

namespace WebLog.Logger.Metrics
{
    /// <summary>
    /// Реализация расширения для предоставления дополнительных
    /// механизмов конфигурирования запросов в pipeline.
    /// </summary>
    public static class MetricsBuilderExtension
    {
        /// <summary>
        /// Установка и настройка метрик для экспорта данных в Prometheus.
        /// </summary>
        /// <returns></returns>
        public static IApplicationBuilder UseMetrics(this IApplicationBuilder builder)
        {
            return builder.ConfigurePrometheus();
        }

        /// <summary>
        /// Starts a Prometheus metrics exporter. The default URL is /metrics, which is a Prometheus convention.
        /// </summary>
        private static IApplicationBuilder ConfigurePrometheus(this IApplicationBuilder builder)            
        {
            builder.Map("/metrics", x => x.UseMetricServer(null));

            var settings = new MetricServerMiddleware.Settings
            {
                Registry = null
            };

            builder.UseMiddleware<MetricServerMiddleware>(settings);

            return builder;
        }
        
        /// <summary>
        /// Add DI service
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMetric(this IServiceCollection services)
        {
            services.AddSingleton<IMetricOptions, MetricOptions>();
            return services.AddSingleton<IMetric, Metric>();
        }

        /// <summary>
        /// Установка и настройка метрики для нового Niddleware. Метрики могут в себя включать следующие типы: Counter, Gauge, Summary, Histogram.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        /// <param name="configureOptions"></param>
        /// <returns></returns>
        public static IApplicationBuilder AddMetric<T>(
            this IApplicationBuilder builder, 
            Action<IMetricOptions> configureOptions
        ) where T : IMetricMiddleware {
            var options = new MetricOptions();
            configureOptions(options);
            return builder.UseMiddleware<T>(options);
        }
    }
}