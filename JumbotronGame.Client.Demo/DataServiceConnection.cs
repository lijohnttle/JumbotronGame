using JumbotronGame.Server.DataContracts;
using JumbotronGame.Server.DataServices;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace JumbotronGame.Client.Demo
{
    public class DataServiceConnection<TEntity, TCriteria> : IDataServiceConnection<TEntity, TCriteria>
        where TEntity : IEntity
        where TCriteria : ICriteria
    {
        #region Fields

        private readonly IDataService<TEntity, TCriteria> _dataService;

        #endregion Fields

        #region Events

        public event EventHandler<DataChangedEventArgs<TEntity>> ItemsAdded;
        public event EventHandler<DataChangedEventArgs<TEntity>> ItemsChanged;
        public event EventHandler<DataChangedEventArgs<TEntity>> ItemsRemoved;

        #endregion Events

        #region .ctor

        public DataServiceConnection(IDataService<TEntity, TCriteria> dataService)
        {
            _dataService = dataService;
        }

        #endregion .ctor

        #region Methods

        public async Task<TEntity> GetItemAsync(int id, CancellationToken ct)
        {
            await Task.Delay(1000);

            return await _dataService.GetItemAsync(id, ct);
        }

        public async Task<IEnumerable<TEntity>> GetItemsAsync(TCriteria criteria, CancellationToken ct)
        {
            await Task.Delay(1000);

            return await _dataService.GetItemsAsync(criteria, ct);
        }

        public async Task<TEntity> SaveItemAsync(TEntity entity, CancellationToken ct)
        {
            await Task.Delay(1000);

            return await _dataService.SaveItemAsync(entity, ct);
        }

        #endregion Methods
    }
}