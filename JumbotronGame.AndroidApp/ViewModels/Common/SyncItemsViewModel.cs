using JumbotronGame.AndroidApp.Repositories;
using JumbotronGame.Client;
using JumbotronGame.Common.Logging;
using JumbotronGame.Server.DataContracts;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JumbotronGame.AndroidApp.ViewModels.Common
{
    public abstract class SyncItemsViewModel<TEntity, TCriteria, TItem> : ExtendedViewModel, ISyncItemsViewModel<TEntity, TCriteria, TItem>
        where TEntity : IEntity
        where TCriteria : ICriteria
        where TItem : IItemViewModel
    {
        #region Fields

        private readonly object _syncRoot = new object();
        private bool _isReceivingNotifications;
        private bool _isDisposed;
        private bool _isSynchronizing;

        #endregion Fields

        #region .ctor

        public SyncItemsViewModel(IRepository<TEntity, TCriteria> repository)
        {
            Repository = repository;

            Items = new ObservableCollection<TItem>();
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

        ~SyncItemsViewModel()
        {
            DisposeCore(true);
        }

        #endregion IDisposable Members

        #region Properties

        public ObservableCollection<TItem> Items { get; }

        public TCriteria Criteria { get; set; }

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

                Items.Clear();

                var data = await Repository.GetItemsAsync(Criteria, CancellationToken.None);

                OnItemsAdded(new DataChangedEventArgs<TEntity>(data));

                IsSynchronizing = false;
            }
            catch (Exception ex)
            {
                LoggingHelper.Logger.Error(ex);
            }
        }

        protected abstract TItem Map(TEntity entity);

        protected abstract void Map(TEntity entity, TItem item);

        protected void StartReceiveNotifications()
        {
            lock (_syncRoot)
            {
                Repository.ItemsAdded -= OnItemsAdded;
                Repository.ItemsChanged -= OnItemsChanged;
                Repository.ItemsRemoved -= OnItemsRemoved;

                _isReceivingNotifications = true;

                Repository.ItemsAdded += OnItemsAdded;
                Repository.ItemsChanged += OnItemsChanged;
                Repository.ItemsRemoved += OnItemsRemoved;
            }
        }

        protected void StopReceiveNotifications()
        {
            lock (_syncRoot)
            {
                _isReceivingNotifications = false;

                Repository.ItemsAdded -= OnItemsAdded;
                Repository.ItemsChanged -= OnItemsChanged;
                Repository.ItemsRemoved -= OnItemsRemoved;
            }
        }

        protected virtual void OnItemsAdded(DataChangedEventArgs<TEntity> e)
        {
            foreach (var entity in e.Entities)
            {
                Items.Add(Map(entity));
            }
        }

        protected virtual void OnItemsChanged(DataChangedEventArgs<TEntity> e)
        {
            foreach (var entity in e.Entities)
            {
                var item = Items.FirstOrDefault(t => t.Id == entity.Id);
                if (item != null)
                {
                    Map(entity, item);
                }
            }
        }

        protected virtual void OnItemsRemoved(DataChangedEventArgs<TEntity> e)
        {
            foreach (var entity in e.Entities)
            {
                var item = Items.FirstOrDefault(t => t.Id == entity.Id);
                if (item != null)
                {
                    Items.Remove(item);
                }
            }
        }

        private void OnItemsAdded(object sender, DataChangedEventArgs<TEntity> e)
        {
            Context.Post(t =>
            {
                if(_isReceivingNotifications)
                {
                    OnItemsAdded(e);
                }
            }, null);
        }

        private void OnItemsChanged(object sender, DataChangedEventArgs<TEntity> e)
        {
            Context.Post(t =>
            {
                if (_isReceivingNotifications)
                {
                    OnItemsChanged(e);
                }
            }, null);
        }

        private void OnItemsRemoved(object sender, DataChangedEventArgs<TEntity> e)
        {
            Context.Post(t =>
            {
                if (_isReceivingNotifications)
                {
                    OnItemsRemoved(e);
                }
            }, null);
        }

        #endregion Methods
    }
}