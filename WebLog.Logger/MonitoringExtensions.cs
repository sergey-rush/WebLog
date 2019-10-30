using System.Reflection;
using WebLog.Logger.Logger;
using WebLog.Logger.Metrics;
using WebLog.Logger.Metrics.Middleware;
using WebLog.Logger.Tracer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebLog.Logger
{
    /// <summary>
    /// Extends application pipeline and services with assigned logger and metric
    /// </summary>
    public static class MonitoringExtension
    {
        /// <summary>
        /// Adds services for logger and metric
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <param name="config">IConfiguration</param>
        /// <returns></returns>
        public static IServiceCollection AddMonitoring(this IServiceCollection services, IConfiguration config)
        {
            string assemblyName = Assembly.GetEntryAssembly().GetName().Name;
            services.AddTracing(options => {
                options.ServiceName = Assembly.GetEntryAssembly().GetName().Name;
            });
            services.AddLogger(config);
            services.AddMetric();
            return services;
        }
        
        /// <summary>
        /// Extends application pipeline with logger and metric
        /// </summary>
        /// <param name="builder">IApplicationBuilder</param>
        /// <returns></returns>
        public static IApplicationBuilder UseMonitoring(this IApplicationBuilder builder)
        {
            
            builder.UseMetrics();
            builder.AddMetric<MetricCounterMiddleware>(x =>
            {
                x.Name = "weblog_counter_request";
                x.Help = "Test request from WebLog";
                x.LabelNames = new[] { "path", "method", "status" };
            });
            builder.UseLogger();
            return builder;
            
        }
    }
}