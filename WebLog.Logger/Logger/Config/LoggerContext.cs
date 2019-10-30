using System;
using System.Reflection;
using System.Threading;

namespace WebLog.Logger.Logger.Config
{
    /// <summary>
    ///     Набор полей логирования для текущего контекста.
    /// </summary>
    public class LoggerContext
    {
        /// <summary>
        ///     Bмя машины / докера
        /// </summary>
        public static AsyncLocal<string> Host = new AsyncLocal<string>();

        /// <summary>
        ///     Название Web метода в рамках которого происходит действие/
        /// </summary>
        public static AsyncLocal<string> Operation = new AsyncLocal<string>();

        /// <summary>
        ///     сквозной идентификатор, позволяющий связать записи с конкретным вызовом.
        /// </summary>
        public static AsyncLocal<string> XOperationId = new AsyncLocal<string>();

        static LoggerContext()
        {
            ContextEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            Service = Assembly.GetCallingAssembly()
                .GetName()
                .Name;
            ServiceName = Assembly.GetEntryAssembly()?.GetName().Name;
        }

        /// <summary>
        ///     Имя контура.
        /// </summary>
        public static string ContextEnvironment { get; }

        /// <summary>
        ///     имя монитор сервиса.
        /// </summary>
        public static string Service { get; }

        /// <summary>
        ///     имя исполнитель сервиса.
        /// </summary>
        public static string ServiceName { get; }

        /// <summary>
        ///     Тип уровня сообщения.
        /// </summary>
        public static string Type { get; set; }

        /// <summary>
        ///     версия сервиса
        /// </summary>
        public static string Version { get; set; }

        /// <summary>
        ///     Флаг того что микросервис в канарейке.
        /// </summary>
        public static int Canary { get; set; } = 1;

        /// <summary>
        ///     Признак публичности.
        /// </summary>
        public static int XPub { get; set; } = 1;
    }
}