using JumbotronGame.Server.DataContracts.Arena;
using JumbotronGame.Server.DataContracts.Game;
using JumbotronGame.Server.DataContracts.Users;
using Microsoft.Extensions.DependencyInjection;

namespace JumbotronGame.Client.Demo.Infrastructure
{
    public static class Bootstrapper
    {
        public static void Configure(IServiceCollection serviceCollection)
        {
            Server.DataServices.Demo.Infrastructure.Bootstrapper.Configure(serviceCollection);

            serviceCollection.AddSingleton<IDataServiceConnection<ArenaEvent, ArenaEventCriteria>, DataServiceConnection<ArenaEvent, ArenaEventCriteria>>();
            serviceCollection.AddSingleton<IDataServiceConnection<Quiz, QuizCriteria>, DataServiceConnection<Quiz, QuizCriteria>>();
            serviceCollection.AddSingleton<IDataServiceConnection<QuizAnswerSet, QuizAnswerSetCriteria>, DataServiceConnection<QuizAnswerSet, QuizAnswerSetCriteria>>();
            serviceCollection.AddSingleton<IDataServiceConnection<UserProfile, UserProfileCriteria>, DataServiceConnection<UserProfile, UserProfileCriteria>>();
        }
    }
}