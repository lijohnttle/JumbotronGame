using JumbotronGame.AndroidApp.Repositories;
using JumbotronGame.AndroidApp.ViewModels.Common;
using JumbotronGame.Server.DataContracts.Arena;

namespace JumbotronGame.AndroidApp.ViewModels.ArenaEvents
{
    public class ArenaEventListViewModel : SyncItemsViewModel<ArenaEvent, ArenaEventCriteria, ArenaEventListItemViewModel>, IArenaEventListViewModel
    {
        #region .ctor

        public ArenaEventListViewModel(IRepository<ArenaEvent, ArenaEventCriteria> repository)
            : base(repository)
        {
            StartReceiveNotifications();
        }

        #endregion .ctor

        #region Methods

        protected override ArenaEventListItemViewModel Map(ArenaEvent entity)
        {
            var result = new ArenaEventListItemViewModel();

            Map(entity, result);

            return result;
        }

        protected override void Map(ArenaEvent entity, ArenaEventListItemViewModel item)
        {
            item.Id = entity.Id;
            item.Header = entity.Header;
            item.Date = entity.Date;
        }

        #endregion Methods
    }
}