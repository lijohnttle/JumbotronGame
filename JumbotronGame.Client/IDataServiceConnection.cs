using JumbotronGame.Server.DataContracts;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace JumbotronGame.Client
{
    public interface IDataServiceConnection<TEntity, TCriteria>
        where TEntity : IEntity
        where TCriteria : ICriteria
    {
        event EventHandler<DataChangedEventArgs<TEntity>> ItemsAdded;
        event EventHandler<DataChangedEventArgs<TEntity>> ItemsChanged;
        event EventHandler<DataChangedEventArgs<TEntity>> ItemsRemoved;

        Task<TEntity> GetItemAsync(int id, CancellationToken ct);
        Task<IEnumerable<TEntity>> GetItemsAsync(TCriteria criteria, CancellationToken ct);
        Task<TEntity> SaveItemAsync(TEntity entity, CancellationToken ct);
    }
}