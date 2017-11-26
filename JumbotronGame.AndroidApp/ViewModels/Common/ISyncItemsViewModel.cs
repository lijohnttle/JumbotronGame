using JumbotronGame.Server.DataContracts;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace JumbotronGame.AndroidApp.ViewModels.Common
{
    public interface ISyncItemsViewModel<TEntity, TCriteria, TItem> : INotifyPropertyChanged, IDisposable
        where TEntity : IEntity
        where TCriteria : ICriteria
        where TItem : IItemViewModel
    {
        ObservableCollection<TItem> Items { get; }
        TCriteria Criteria { get; set; }
        bool IsSynchronizing { get; }

        void Synchronize();
        Task SynchronizeAsync();
    }
}