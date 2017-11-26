using GalaSoft.MvvmLight;
using System.Threading;

namespace JumbotronGame.AndroidApp.ViewModels.Common
{
    public abstract class ExtendedViewModel : ViewModelBase
    {
        #region .ctor

        protected ExtendedViewModel()
        {
            Context = SynchronizationContext.Current;
        }

        #endregion .ctor

        #region Properties

        protected SynchronizationContext Context { get; }

        #endregion Properties
    }
}