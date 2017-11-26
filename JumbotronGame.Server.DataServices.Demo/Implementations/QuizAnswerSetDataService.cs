using JumbotronGame.Server.DataContracts.Game;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace JumbotronGame.Server.DataServices.Demo.Implementations
{
    public class QuizAnswerSetDataService : DataServiceBase<QuizAnswerSet, QuizAnswerSetCriteria>
    {
        #region Methods

        public override async Task<QuizAnswerSet> GetItemAsync(int id, CancellationToken ct)
        {
            return await Task.FromResult<QuizAnswerSet>(null).ConfigureAwait(false);
        }

        public override async Task<IEnumerable<QuizAnswerSet>> GetItemsAsync(QuizAnswerSetCriteria criteria, CancellationToken ct)
        {
            return await Task.FromResult(new QuizAnswerSet[0]).ConfigureAwait(false);
        }

        public override async Task<QuizAnswerSet> SaveItemAsync(QuizAnswerSet entity, CancellationToken ct)
        {
            return await Task.FromResult(entity).ConfigureAwait(false);
        }

        #endregion Methods
    }
}