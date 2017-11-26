using JumbotronGame.Server.DataContracts.Game;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JumbotronGame.Server.DataServices.Demo.Implementations
{
    public class QuizDataService : DataServiceBase<Quiz, QuizCriteria>
    {
        #region Fields

        private readonly ConcurrentDictionary<int, Quiz> _storage = new ConcurrentDictionary<int, Quiz>();

        #endregion Fields

        #region .ctor

        public QuizDataService()
        {
            for (var i = 0; i < 3; i++)
            {
                _storage.TryAdd(i, new Quiz
                {
                    Id = i,
                    Questions = Enumerable.Range(0, 5).Select(t =>
                    {
                        return new QuizQuestion
                        {
                            Question = $"Some question #{t}?",
                            AnswerA = "Answer A",
                            AnswerB = "Answer B",
                            AnswerC = "Answer C",
                            AnswerD = "Answer D"
                        };
                    }).ToArray()
                });
            }
        }

        #endregion .ctor

        #region Methods

        public override async Task<Quiz> GetItemAsync(int id, CancellationToken ct)
        {
            if (_storage.TryGetValue(id, out var result))
            {
                return await Task.FromResult(result).ConfigureAwait(false);
            }

            return await Task.FromResult<Quiz>(null).ConfigureAwait(false);
        }

        public override async Task<IEnumerable<Quiz>> GetItemsAsync(QuizCriteria criteria, CancellationToken ct)
        {
            var result = new List<Quiz>();

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

        public override Task<Quiz> SaveItemAsync(Quiz entity, CancellationToken ct)
        {
            throw new System.NotImplementedException();
        }

        #endregion Methods
    }
}