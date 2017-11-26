using JumbotronGame.Common.Logging;
using System.Diagnostics;

namespace JumbotronGame.AndroidApp.Infrastructure.Logging
{
    public class DemoLogger : ILogger
    {
        public void Log(LoggerLevel level, string message)
        {
            switch (level)
            {
                case LoggerLevel.Trace:
                    Trace.WriteLine(message);
                    break;

                default:
                    Debug.WriteLine(message);
                    break;
            }
        }
    }
}