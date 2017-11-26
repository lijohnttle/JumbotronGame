using JumbotronGame.Common.Logging;
using System.Diagnostics;

namespace JumbotronGame.AndroidApp.Infrastructure.Logging
{
    public class DemoLogger : ILogger
    {
        public void Log(LoggerLevel level, string message)
        {
            if (level == LoggerLevel.Trace)
            {
                Trace.WriteLine(message);
            }
            else
            {
                Debug.WriteLine(message);
            }
        }
    }
}