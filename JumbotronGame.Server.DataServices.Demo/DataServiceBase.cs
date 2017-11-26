using JumbotronGame.Server.DataContracts;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace JumbotronGame.Server.DataServices.Demo
{
    public abstract class DataServiceBase<TEntity, TCriteria> : IDataService<TEntity, TCriteria>
        where TEntity : IEntity
        where TCriteria : ICriteria
    {
        public abstract Task<TEntity> GetItemAsync(int id, CancellationToken ct);

        public abstract Task<IEnumerable<TEntity>> GetItemsAsync(TCriteria criteria, CancellationToken ct);

        public abstract Task<TEntity> SaveItemAsync(TEntity entity, CancellationToken ct);
    }
}