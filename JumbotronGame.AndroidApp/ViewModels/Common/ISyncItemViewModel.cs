using JumbotronGame.Server.DataContracts;
using System;
using System.Threading.Tasks;

namespace JumbotronGame.AndroidApp.ViewModels.Common
{
    public interface ISyncItemViewModel<TEntity> : IItemViewModel, IDisposable
        where TEntity : IEntity
    {
        bool IsSynchronizing { get; }

        void Synchronize();
        Task SynchronizeAsync();
    }
}