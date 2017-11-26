namespace JumbotronGame.Common.Logging
{
    public interface ILogger
    {
        void Log(LoggerLevel level, string message);
    }
}