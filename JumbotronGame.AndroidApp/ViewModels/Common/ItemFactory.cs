using Microsoft.Extensions.DependencyInjection;
using System;

namespace JumbotronGame.AndroidApp.ViewModels.Common
{
    public class ItemFactory<TItem> : IItemFactory<TItem>
    {
        #region Fields

        private readonly IServiceProvider _serviceProvider;

        #endregion Fields

        #region .ctor

        public ItemFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        #endregion .ctor

        #region Methods

        public TItem Create()
        {
            return _serviceProvider.GetService<TItem>();
        }

        #endregion Methods
    }
}