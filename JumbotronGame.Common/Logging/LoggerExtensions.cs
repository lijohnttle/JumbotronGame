using System;

namespace JumbotronGame.Common.Logging
{
    public static class LoggerExtensions
    {
        public static void Trace(this ILogger logger, string message)
        {
            logger.Log(LoggerLevel.Trace, message);
        }

        public static void Trace(this ILogger logger, string format, params object[] parameters)
        {
            logger.Log(LoggerLevel.Trace, string.Format(format, parameters));
        }

        public static void Debug(this ILogger logger, string message)
        {
            logger.Log(LoggerLevel.Debug, message);
        }

        public static void Debug(this ILogger logger, string format, params object[] parameters)
        {
            logger.Log(LoggerLevel.Debug, string.Format(format, parameters));
        }

        public static void Info(this ILogger logger, string message)
        {
            logger.Log(LoggerLevel.Info, message);
        }

        public static void Info(this ILogger logger, string format, params object[] parameters)
        {
            logger.Log(LoggerLevel.Info, string.Format(format, parameters));
        }

        public static void Warning(this ILogger logger, string message)
        {
            logger.Log(LoggerLevel.Warning, message);
        }

        public static void Warning(this ILogger logger, string format, params object[] parameters)
        {
            logger.Log(LoggerLevel.Warning, string.Format(format, parameters));
        }

        public static void Error(this ILogger logger, string message)
        {
            logger.Log(LoggerLevel.Error, message);
        }

        public static void Error(this ILogger logger, string format, params object[] parameters)
        {
            logger.Log(LoggerLevel.Error, string.Format(format, parameters));
        }

        public static void Error(this ILogger logger, Exception exception)
        {
            logger.Log(LoggerLevel.Error, exception.Message);
        }
    }
}
