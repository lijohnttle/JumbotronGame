using JumbotronGame.AndroidApp.ViewModels.ArenaEvents;
using System.ComponentModel;

namespace JumbotronGame.AndroidApp.ViewModels.Main
{
    public interface IMainViewModel : INotifyPropertyChanged
    {
        IArenaEventListViewModel ArenaEvents { get; }
    }
}