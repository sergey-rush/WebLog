using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebLog.Logger.Metrics.Middleware
{
    /// <summary>
    /// Добавление промежуточных метрик для Prometheus.
    /// Данный экземпляр описывает реализацию middleware компонента.
    /// </summary>
    public interface IMetricMiddleware
    {
        /// <summary>
        /// Вызов текущего контекса.
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        Task Invoke(HttpContext httpContext);
    }
}