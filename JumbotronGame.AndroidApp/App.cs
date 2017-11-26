using Android.App;
using Android.Runtime;
using JumbotronGame.AndroidApp.Infrastructure.Logging;
using JumbotronGame.AndroidApp.Repositories;
using JumbotronGame.AndroidApp.ViewModels;
using JumbotronGame.AndroidApp.ViewModels.ArenaEvents;
using JumbotronGame.AndroidApp.ViewModels.Common;
using JumbotronGame.AndroidApp.ViewModels.Main;
using JumbotronGame.AndroidApp.ViewModels.Users;
using JumbotronGame.Common.Logging;
using JumbotronGame.Server.DataContracts.Arena;
using JumbotronGame.Server.DataContracts.Game;
using JumbotronGame.Server.DataContracts.Users;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace JumbotronGame.AndroidApp
{
    [Application(Theme = "@style/MyCustomTheme")]
    public class App : Application
    {
        #region Fields

        private IServiceProvider _serviceProvider;

        #endregion Fields

        #region .ctor

        public App(IntPtr handle, JniHandleOwnership ownerShip)
            : base(handle, ownerShip)
        {

        }

        #endregion .ctor

        #region Properties

        public static ViewModelLocator Locator { get; private set; }

        #endregion Properties

        #region Methods

        public override void OnCreate()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            _serviceProvider = serviceCollection.BuildServiceProvider();

            Locator = new ViewModelLocator(_serviceProvider);

            InitializeServices();

            base.OnCreate();
        }

        private void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ILogger, DemoLogger>();

            Client.Demo.Infrastructure.Bootstrapper.Configure(serviceCollection);

            #region Repositories

            serviceCollection.AddSingleton<IRepository<ArenaEvent, ArenaEventCriteria>, Repository<ArenaEvent, ArenaEventCriteria>>();
            serviceCollection.AddSingleton<IRepository<Quiz, QuizCriteria>, Repository<Quiz, QuizCriteria>>();
            serviceCollection.AddSingleton<IRepository<QuizAnswerSet, QuizAnswerSetCriteria>, Repository<QuizAnswerSet, QuizAnswerSetCriteria>>();
            serviceCollection.AddSingleton<IRepository<UserProfile, UserProfileCriteria>, Repository<UserProfile, UserProfileCriteria>>();

            #endregion Repositories

            #region ViewModels

            serviceCollection.AddSingleton<IArenaEventListViewModel, ArenaEventListViewModel>();
            serviceCollection.AddTransient<ArenaEventViewModel>();
            serviceCollection.AddTransient<UserProfileViewModel>();
            serviceCollection.AddSingleton<IItemFactory<ArenaEventViewModel>, ItemFactory<ArenaEventViewModel>>();
            serviceCollection.AddSingleton<IItemFactory<UserProfileViewModel>, ItemFactory<UserProfileViewModel>>();

            serviceCollection.AddSingleton<IMainViewModel, MainViewModel>();

            #endregion ViewModels
        }

        private void InitializeServices()
        {
            LoggingHelper.Initialize(_serviceProvider.GetService<ILogger>());
        }

        #endregion Methods
    }
}