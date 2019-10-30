using System;
using System.Collections.Generic;
using WebLog.Logger.Logger.Config;
using WebLog.Logger.Metrics;
using NLog;

namespace WebLog.Logger.Logger
{
    /// <inheritdoc cref="ILogger" />
    public class Logger : ILogger
    {
        private static NLog.ILogger _logger = LogManager.GetCurrentClassLogger();
        private IMetric metric;

        /// <summary>
        /// Inits Logger instance
        /// </summary>
        /// <param name="metric">IMetric</param>
        public Logger(IMetric metric)
        {
            this.metric = metric;
        }

        /// <inheritdoc />
        public void Information(string message)
        {
            LogInternal(message, LogLevel.Info);
        }

        /// <inheritdoc />
        public void Warning(string message)
        {
            LogInternal(message, LogLevel.Warn);
        }

        /// <inheritdoc />
        public void Debug(string message)
        {
            LogInternal(message, LogLevel.Debug);
        }

        /// <inheritdoc />
        public void Error(string message)
        {
            LogInternal(message, LogLevel.Error);
        }

        /// <inheritdoc />
        public void Fatal(string message)
        {
            LogInternal(message, LogLevel.Fatal);
        }

        /// <inheritdoc />
        public void Log<TState>(LogLevel logLevel, string eventId, TState state, Exception exception,
            Func<TState, Exception, string> formatter)
        {
            _logger.Log(logLevel, eventId, state, exception, formatter);
        }

        private void LogInternal(string message, LogLevel logLevel, Exception exception = null)
        {
            var logEvent = new LogEventInfo(logLevel, null, message);

            logEvent.Properties["weblog_environment"] = LoggerContext.ContextEnvironment;
            logEvent.Properties["weblog_host"] = LoggerContext.Host.Value;
            logEvent.Properties["weblog_operation"] = LoggerContext.Operation.Value;
            logEvent.Properties["weblog_x_operation_id"] = LoggerContext.XOperationId.Value;
            logEvent.Properties["weblog_service"] = LoggerContext.ServiceName;
            logEvent.Properties["weblog_type"] = logLevel.ToString();
            logEvent.Properties["weblog_canary"] = LoggerContext.Canary;
            logEvent.Properties["weblog_x_pub"] = LoggerContext.XPub;

            _logger.Log(logEvent);
            AppendCounter(logEvent);
        }

        /// <summary>
        /// Appends counter metric with LogEventInfo
        /// </summary>
        /// <param name="logEvent"></param>
        private void AppendCounter(LogEventInfo logEvent)
        {
            List<string> labelNames = new List<string>();
            List<string> labelValues = new List<string>();
            
            foreach (var item in logEvent.Properties)
            {
                labelNames.Add(item.Key.ToString());
                
                var value =  item.Value ?? string.Empty;
                labelValues.Add(value.ToString());
            }
            
            metric.IncrementCounter(x =>
            {
                x.Name = "service_health";
                x.Help = $"Trace from {LoggerContext.ServiceName}";
                x.LabelNames = labelNames.ToArray();
                x.LabelValues = labelValues.ToArray();
            });
        }
    }
}