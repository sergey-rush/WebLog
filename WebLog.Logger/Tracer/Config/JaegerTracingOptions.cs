using Microsoft.Extensions.Logging;

namespace WebLog.Logger.Tracer.Config
{
    /// <inheritdoc cref="ITracerOptions" />
    public class JaegerTracingOptions : ITracerOptions
    {
        /// <inheritdoc />
        public double SamplingRate { get; set; }

        /// <inheritdoc />
        public double LowerBound { get; set; }

        /// <inheritdoc />
        public ILoggerFactory LoggerFactory { get; set; }

        /// <inheritdoc />
        public string JaegerAgentHost { get; set; }

        /// <inheritdoc />
        public int JaegerAgentPort { get; set; }

        /// <inheritdoc />
        public string ServiceName { get; set; }

        /// <summary>
        /// ctor.
        /// </summary>
        public JaegerTracingOptions()
        {
            SamplingRate = 0.1d;
            LowerBound = 1d;
            LoggerFactory = new LoggerFactory();
            JaegerAgentHost = "localhost";
            JaegerAgentPort = 6831;
        }
    }
}