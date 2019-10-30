using System;
using WebLog.Logger.Tracer.Config;
using Jaeger;
using Jaeger.Reporters;
using Jaeger.Samplers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OpenTracing;
using OpenTracing.Util;

namespace WebLog.Logger.Tracer
{
    /// <summary>
    /// Реализация расширения дескрипторв сервисов для подключения
    /// дополнительных библиотек.
    /// </summary>
    public static class TracerBuilderExtension
    {
        /// <summary>
        /// Подключение и настройка экземпляра объекта Jaeger.Tracer.
        /// С помощью AddSingleton() для добавления нового сервиса в 
        /// контейнер дескрипторов.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="setupAction"></param>
        /// <returns></returns>
        public static IServiceCollection AddTracing(
            this IServiceCollection services,
            Action<JaegerTracingOptions> setupAction = null)
        {
            if (setupAction != null)
            {
                services.ConfigureTracing(setupAction);
            }

            services.AddSingleton<ITracer>(x => {
                var options = x.GetService<IOptions<JaegerTracingOptions>>().Value;

                var tracer = ConfigureJaegerTracer(options);

                if (!GlobalTracer.IsRegistered())
                {
                    GlobalTracer.Register(tracer);
                }

                return tracer;
            });

            services.AddOpenTracing();

            return services;
        }

        /// <summary>
        /// Добавление настроек для Jaeger.Tracer.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="setupAction"></param>
        public static void ConfigureTracing(
            this IServiceCollection services,
            Action<JaegerTracingOptions> setupAction)
        {
            services.Configure<JaegerTracingOptions>(setupAction);
        }

        /// <summary>
        /// Построение нового экземпляра класса Jaeger.Tracer для 
        /// отправки запросов и метрики в удаленный сервис-агента.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public static ITracer ConfigureJaegerTracer(ITracerOptions options = null)
        {
            var senderConfig = new Configuration.SenderConfiguration(options.LoggerFactory)
                .WithAgentHost(options.JaegerAgentHost)
                .WithAgentPort(options.JaegerAgentPort);

            var reporter = new RemoteReporter.Builder()
                .WithLoggerFactory(options.LoggerFactory)
                .WithSender(senderConfig.GetSender())
                .Build();

            var sampler = new GuaranteedThroughputSampler(options.SamplingRate, options.LowerBound);

            return new Jaeger.Tracer.Builder(options.ServiceName)
                .WithLoggerFactory(options.LoggerFactory)
                .WithReporter(reporter)
                .WithSampler(sampler)
                .Build();
        }
    }
}