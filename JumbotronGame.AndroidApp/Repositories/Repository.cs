using JumbotronGame.Client;
using JumbotronGame.Server.DataContracts;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace JumbotronGame.AndroidApp.Repositories
{
    public class Repository<TEntity, TCriteria> : IRepository<TEntity, TCriteria>
        where TEntity : IEntity
        where TCriteria : ICriteria
    {
        #region Fields

        private readonly IDataServiceConnection<TEntity, TCriteria> _dataServiceConnection;

        #endregion Fields

        #region Events

        public event EventHandler<DataChangedEventArgs<TEntity>> ItemsAdded;
        public event EventHandler<DataChangedEventArgs<TEntity>> ItemsChanged;
        public event EventHandler<DataChangedEventArgs<TEntity>> ItemsRemoved;

        #endregion Events

        #region .ctor

        public Repository(IDataServiceConnection<TEntity, TCriteria> dataServiceConnection)
        {
            _dataServiceConnection = dataServiceConnection;

            _dataServiceConnection.ItemsAdded += OnItemsAdded;
            _dataServiceConnection.ItemsChanged += OnItemsChanged;
            _dataServiceConnection.ItemsRemoved += OnItemsRemoved;
        }

        #endregion .ctor

        #region Methods

        public async Task<TEntity> GetItemAsync(int id, CancellationToken ct)
        {
            return await _dataServiceConnection.GetItemAsync(id, ct);
        }

        public async Task<IEnumerable<TEntity>> GetItemsAsync(TCriteria criteria, CancellationToken ct)
        {
            return await _dataServiceConnection.GetItemsAsync(criteria, ct);
        }

        public async Task<TEntity> SaveItemAsync(TEntity entity, CancellationToken ct)
        {
            return await _dataServiceConnection.SaveItemAsync(entity, ct);
        }

        private void OnItemsAdded(object sender, DataChangedEventArgs<TEntity> e)
        {
            ItemsAdded?.Invoke(this, e);
        }

        private void OnItemsChanged(object sender, DataChangedEventArgs<TEntity> e)
        {
            ItemsChanged?.Invoke(this, e);
        }

        private void OnItemsRemoved(object sender, DataChangedEventArgs<TEntity> e)
        {
            ItemsRemoved?.Invoke(this, e);
        }

        #endregion Methods
    }
}