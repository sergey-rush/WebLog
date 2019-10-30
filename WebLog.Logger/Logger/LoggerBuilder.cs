using System;
using System.Collections;
using System.Collections.Generic;

namespace WebLog.Logger.Logger
{
    /// <summary>
    /// Экземпляр класса логирования.    
    /// </summary>
    /// <inheritdoc cref="IEnumerable" />
    public class LoggerBuilder : IEnumerable<KeyValuePair<string, object>>
    {
        readonly List<KeyValuePair<string, object>> _properties = new List<KeyValuePair<string, object>>();

        public string Message { get; }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="message"></param>
        public LoggerBuilder(string message)
        {
            Message = message;
        }

        /// <summary>
        /// Добавление новых свойств.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public LoggerBuilder AddProp(string name, object value)
        {
            _properties.Add(new KeyValuePair<string, object>(name, value));
            return this;
        }

        /// <summary>
        /// Делегат вывода сообщдения.
        /// </summary>
        public static Func<LoggerBuilder, Exception, string> Formatter { get; } = (l, e) => l.Message;

        /// <inheritdoc />
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return _properties.GetEnumerator();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
    }
}