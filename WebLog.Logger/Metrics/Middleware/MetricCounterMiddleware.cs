using System;
using System.Threading.Tasks;
using WebLog.Logger.Metrics.Config;
using Microsoft.AspNetCore.Http;
using Prometheus;

namespace WebLog.Logger.Metrics.Middleware
{
    /// <inheritdoc cref="IMetricMiddleware" />
    public class MetricCounterMiddleware : IMetricMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMetricOptions _options;

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="next"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="options"></param>
        public MetricCounterMiddleware(RequestDelegate next, IMetricOptions options)
        {
            _next = next;            
            _options = options;
        }

        /// <inheritdoc />
        public async Task Invoke(HttpContext httpContext)
        {
            var path = httpContext.Request.Path.Value;
            var method = httpContext.Request.Method;

            var counter = Prometheus.Metrics.CreateCounter(_options.Name, _options.Help, new CounterConfiguration
            {                
                LabelNames = _options.LabelNames
            });

            try
            {
                await _next.Invoke(httpContext);
            }
            catch (Exception)
            {
                counter.Labels(path, method, StatusCodes.Status500InternalServerError.ToString()).Inc();

                throw;
            }

            if (path != "/metrics")
            {
                counter.Labels(path, method, httpContext.Response.StatusCode.ToString()).Inc();
            }
        }
    }
}