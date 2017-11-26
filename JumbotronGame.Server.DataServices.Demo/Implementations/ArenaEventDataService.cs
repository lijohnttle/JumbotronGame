using JumbotronGame.Server.DataContracts.Arena;
using JumbotronGame.Server.DataContracts.Game;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JumbotronGame.Server.DataServices.Demo.Implementations
{
    public class ArenaEventDataService : DataServiceBase<ArenaEvent, ArenaEventCriteria>
    {
        #region Fields

        private ConcurrentDictionary<int, ArenaEvent> _storage = new ConcurrentDictionary<int, ArenaEvent>();
        private readonly IDataService<Quiz, QuizCriteria> _quizDataService;

        #endregion Fields

        #region .ctor

        public ArenaEventDataService(IDataService<Quiz, QuizCriteria> quizDataService)
        {
            _quizDataService = quizDataService;

            var id = 0;

            _storage.TryAdd(id, new ArenaEvent
            {
                Id = 0,
                Header = "Save-On-Foods Memorial Centre",
                Quiz = _quizDataService.GetItemAsync(id, CancellationToken.None).Result,
                Date = DateTime.Now
            });

            id = 1;

            _storage.TryAdd(id, new ArenaEvent
            {
                Id = id,
                Header = "Prospera Place",
                Quiz = _quizDataService.GetItemAsync(1, CancellationToken.None).Result,
                Date = DateTime.Now
            });

            id = 2;

            _storage.TryAdd(id, new ArenaEvent
            {
                Id = id,
                Header = "Victoria Royals",
                Quiz = _quizDataService.GetItemAsync(id, CancellationToken.None).Result,
                Date = DateTime.Now
            });
        }

        #endregion .ctor

        #region Methods

        public override async Task<ArenaEvent> GetItemAsync(int id, CancellationToken ct)
        {
            if (_storage.TryGetValue(id, out var result))
            {
                return await Task.FromResult(result).ConfigureAwait(false);
            }

            return await Task.FromResult<ArenaEvent>(null).ConfigureAwait(false);
        }

        public override async Task<IEnumerable<ArenaEvent>> GetItemsAsync(ArenaEventCriteria criteria, CancellationToken ct)
        {
            var result = new List<ArenaEvent>();

            foreach (var entity in _storage.Values)
            {
                if (criteria != null && criteria.Ids != null)
                {
                    if (!criteria.Ids.Contains(entity.Id))
                    {
                        continue;
                    }
                }

                result.Add(entity);
            }

            return await Task.FromResult(result).ConfigureAwait(false);
        }

        public override Task<ArenaEvent> SaveItemAsync(ArenaEvent entity, CancellationToken ct)
        {
            throw new System.NotImplementedException();
        }

        #endregion Methods
    }
}