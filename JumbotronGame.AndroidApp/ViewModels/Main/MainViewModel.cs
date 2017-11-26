using GalaSoft.MvvmLight;
using JumbotronGame.AndroidApp.ViewModels.ArenaEvents;

namespace JumbotronGame.AndroidApp.ViewModels.Main
{
    public class MainViewModel : ViewModelBase, IMainViewModel
    {
        #region .ctor

        public MainViewModel(IArenaEventListViewModel arenaEvents)
        {
            ArenaEvents = arenaEvents;
        }

        #endregion .ctor

        #region Properties

        public IArenaEventListViewModel ArenaEvents { get; }

        #endregion Properties
    }
}