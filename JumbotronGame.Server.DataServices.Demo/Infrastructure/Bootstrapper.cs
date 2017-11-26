using JumbotronGame.Server.DataContracts.Arena;
using JumbotronGame.Server.DataContracts.Game;
using JumbotronGame.Server.DataServices.Demo.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace JumbotronGame.Server.DataServices.Demo.Infrastructure
{
    public static class Bootstrapper
    {
        public static void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IDataService<ArenaEvent, ArenaEventCriteria>, ArenaEventDataService>();
            serviceCollection.AddSingleton<IDataService<Quiz, QuizCriteria>, QuizDataService>();
            serviceCollection.AddSingleton<IDataService<QuizAnswerSet, QuizAnswerSetCriteria>, QuizAnswerSetDataService>();
        }
    }
}