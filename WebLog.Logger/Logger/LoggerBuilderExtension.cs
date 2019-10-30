using System.IO;
using WebLog.Logger.Logger.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace WebLog.Logger.Logger
{
    /// <summary>
    ///     Расширение .NET Core конфигурации для логирования текущего приложения.
    /// </summary>
    public static class LoggerBuilderExtension
    {
        /// <summary>
        ///     Расширение основной конфигурвции с дополнительными параметрами логирования.
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IConfiguration ConfigureLogger(this IConfiguration configuration)
        {
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/Logger/Config/NLog.config"));
            return configuration;
        }

        /// <summary>
        ///     Расширение для включения логирования.
        ///     Подключение дополнительных сервисов логирования в общий контейнер.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddLogger(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<ILogger, Logger>();
            services.AddSingleton<LoggerBuilder>();

            services.AddLogging(x =>
            {
                x.ClearProviders();
                x.SetMinimumLevel(LogLevel.Trace);
                x.AddNLog(config);
            });

            return services;
        }


        /// <summary>
        ///     Расширение для включения логирования.
        ///     Подключение дополнительных настроек при конфигурировании окружения.
        /// </summary>
        /// <param name="env"></param>
        /// <returns></returns>
        public static IHostingEnvironment Configure(this IHostingEnvironment env)
        {
            return env;
        }

        /// <summary>
        ///     Расширение для включения логирования.
        ///     Подключение дополнительных механизмов управления pipelines для текущего приложения.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseLogger(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<LoggerMiddleware>();
            return builder;
        }
    }
}