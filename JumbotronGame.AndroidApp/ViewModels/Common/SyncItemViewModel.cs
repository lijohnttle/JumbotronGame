using JumbotronGame.AndroidApp.Repositories;
using JumbotronGame.Client;
using JumbotronGame.Common.Logging;
using JumbotronGame.Server.DataContracts;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JumbotronGame.AndroidApp.ViewModels.Common
{
    public abstract class SyncItemViewModel<TEntity, TCriteria> : ExtendedViewModel, ISyncItemViewModel<TEntity>
        where TEntity : IEntity
        where TCriteria : ICriteria
    {
        #region Fields

        private readonly object _syncRoot = new object();
        private bool _isReceivingNotifications;
        private bool _isDisposed;
        private int _id;
        private bool _isSynchronizing;

        #endregion Fields

        #region .ctor

        protected SyncItemViewModel(IRepository<TEntity, TCriteria> repository)
        {
            Repository = repository;
        }

        #endregion .ctor

        #region IDisposable Members

        public void Dispose()
        {
            DisposeCore(true);
        }

        protected virtual void Dispose(bool disposing)
        {

        }

        private void DisposeCore(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            lock (_syncRoot)
            {
                if (_isDisposed)
                {
                    return;
                }

                try
                {
                    if (disposing)
                    {
                        StopReceiveNotifications();
                    }

                    Dispose(disposing);
                }
                catch (Exception e)
                {
                    LoggingHelper.Logger.Error(e);
                }
                finally
                {
                    _isDisposed = true;

                    if (disposing)
                    {
                        GC.SuppressFinalize(this);
                    }
                }
            }
        }

        ~SyncItemViewModel()
        {
            DisposeCore(true);
        }

        #endregion IDisposable Members

        #region Properties

        public int Id
        {
            get => _id;
            set => Set(ref _id, value);
        }

        public bool IsSynchronizing
        {
            get => _isSynchronizing;
            private set => Set(ref _isSynchronizing, value);
        }

        protected IRepository<TEntity, TCriteria> Repository { get; }

        #endregion Properties

        #region Methods

        public async void Synchronize()
        {
            await SynchronizeAsync();
        }

        public async Task SynchronizeAsync()
        {
            try
            {
                IsSynchronizing = true;

                var data = await Repository.GetItemAsync(Id, CancellationToken.None);

                OnItemChanged(data);

                IsSynchronizing = false;
            }
            catch (Exception ex)
            {
                LoggingHelper.Logger.Error(ex);
            }
        }

        protected abstract void Map(TEntity entity);

        protected void StartReceiveNotifications()
        {
            lock (_syncRoot)
            {
                Repository.ItemsChanged -= OnItemsChanged;
                Repository.ItemsRemoved -= OnItemsRemoved;

                _isReceivingNotifications = true;

                Repository.ItemsChanged += OnItemsChanged;
                Repository.ItemsRemoved += OnItemsRemoved;
            }
        }

        protected void StopReceiveNotifications()
        {
            lock (_syncRoot)
            {
                _isReceivingNotifications = false;

                Repository.ItemsChanged -= OnItemsChanged;
                Repository.ItemsRemoved -= OnItemsRemoved;
            }
        }

        protected virtual void OnItemChanged(TEntity entity)
        {
            Map(entity);
        }

        protected virtual void OnItemRemoved(TEntity entity)
        {
            StopReceiveNotifications();
        }

        private void OnItemsChanged(object sender, DataChangedEventArgs<TEntity> e)
        {
            Context.Post(t =>
            {
                if (_isReceivingNotifications)
                {
                    var entity = e.Entities.FirstOrDefault(p => p.Id == Id);
                    if (entity != null)
                    {
                        OnItemChanged(entity);
                    }
                }
            }, null);
        }

        private void OnItemsRemoved(object sender, DataChangedEventArgs<TEntity> e)
        {
            Context.Post(t =>
            {
                if (_isReceivingNotifications)
                {
                    var entity = e.Entities.FirstOrDefault(p => p.Id == Id);
                    if (entity != null)
                    {
                        OnItemRemoved(entity);
                    }
                }
            }, null);
        }

        #endregion Methods
    }
}