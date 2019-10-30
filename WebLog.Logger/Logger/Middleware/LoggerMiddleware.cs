using System.Threading.Tasks;
using WebLog.Logger.Logger.Config;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NLog;

namespace WebLog.Logger.Logger.Middleware
{
    /// <summary>
    /// Добавление промежуточного логирования через Middleware.
    /// </summary>
    public class LoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="next"></param>
        /// <param name="logger"></param>
        public LoggerMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Обработка текущего вызова и переход к следующему следующего делегату/middleware в pipeline.
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext)
        {
            var eventId = default(EventId).ToString();

            LoggerContext.Host.Value = httpContext.Request.Host.ToString();
            LoggerContext.Operation.Value = httpContext.Request.Method;
            LoggerContext.XOperationId.Value = eventId;

            _logger.Debug("Logger: ");

            await _next.Invoke(httpContext);
        }        
    }
}