using JumbotronGame.Server.DataContracts;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace JumbotronGame.Server.DataServices
{
    public interface IDataService<TEntity, TCriteria>
        where TEntity : IEntity
        where TCriteria : ICriteria
    {
        Task<TEntity> GetItemAsync(int id, CancellationToken ct);
        Task<IEnumerable<TEntity>> GetItemsAsync(TCriteria criteria, CancellationToken ct);
        Task<TEntity> SaveItemAsync(TEntity entity, CancellationToken ct);
    }
}
