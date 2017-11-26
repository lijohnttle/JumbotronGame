using JumbotronGame.Common.Logging;

namespace JumbotronGame.AndroidApp.ViewModels
{
    public static class LoggingHelper
    {
        #region Fields

        private static readonly object _syncRoot = new object();

        #endregion Fields

        #region Properties

        public static ILogger Logger { get; private set; }

        #endregion Properties

        #region Methods

        public static void Initialize(ILogger logger)
        {
            lock (_syncRoot)
            {
                Logger = logger;
            }
        }

        #endregion Methods
    }
}