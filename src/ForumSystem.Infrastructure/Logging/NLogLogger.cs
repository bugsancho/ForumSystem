namespace ForumSystem.Infrastructure.Logging
{
    using System;

    using Newtonsoft.Json;

    using NLog;

    using ILogger = ForumSystem.Core.Logging.ILogger;

    public class NLogLogger : ILogger
    {
        private readonly NLog.Logger _logger;

        public NLogLogger()
        {
            _logger = LogManager.GetLogger("ForumSystem");
        }

        public void Debug(string message, object data = null)
        {
            Log(LogLevel.Debug, message, data);
        }

        public void Info(string message, object data = null)
        {
            Log(LogLevel.Info, message, data);
        }

        public void Warning(string message, object data = null)
        {
            Log(LogLevel.Warn, message, data);
        }

        public void Error(Exception exception, object data = null)
        {
            Log(LogLevel.Error, exception.ToString(), data);
        }

        private void Log(LogLevel logLevel, string message, object data = null)
        {
            if (data != null)
            {
                message += " " + SerializeData(data);
            }

            LogEventInfo logEvent = new LogEventInfo(logLevel, _logger.Name, message);
            _logger.Log(typeof(NLogLogger), logEvent);
        }

        private string SerializeData(object data)
        {
            string serializedMessage = data as string;

            if (serializedMessage == null)
            {
                try
                {
                    serializedMessage = JsonConvert.SerializeObject(data);
                }
                catch (Exception exception)
                {
                    _logger.Error("Error occured while trying to serialize log data");
                    _logger.Error(exception);

                }
            }

            return serializedMessage;
        }
    }
}
