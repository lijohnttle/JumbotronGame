using JumbotronGame.AndroidApp.ViewModels.Common;
using JumbotronGame.Server.DataContracts.Arena;

namespace JumbotronGame.AndroidApp.ViewModels.ArenaEvents
{
    public interface IArenaEventListViewModel : ISyncItemsViewModel<ArenaEvent, ArenaEventCriteria, ArenaEventListItemViewModel>
    {

    }
}