using System;

namespace WebLog.Logger.Logger
{
    /// <summary>
    /// Экземпляр класса с дополнительными функциями логирования.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Отображение сообщения Info уровня.
        /// </summary>
        /// <param name="message"></param>
        void Information(string message);

        /// <summary>
        /// Отображение сообщения Warning уровня.
        /// </summary>
        /// <param name="message"></param>
        void Warning(string message);

        /// <summary>
        /// Отображение сообщения Debug уровня.
        /// </summary>
        /// <param name="message"></param>
        void Debug(string message);

        /// <summary>
        /// Отображение сообщения Error уровня.
        /// </summary>
        /// <param name="message"></param>
        void Error(string message);

        /// <summary>
        /// Отображение сообщения с параметрами.
        /// </summary>
        /// <param name="message"></param>
        void Log<TState>(NLog.LogLevel logLevel, string eventId, TState state, Exception exception, Func<TState, Exception, string> formatter);
    }
}