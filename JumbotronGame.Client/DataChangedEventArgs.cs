using System;
using System.Collections.Generic;
using System.Linq;

namespace JumbotronGame.Client
{
    public class DataChangedEventArgs<TEntity> : EventArgs
    {
        #region .ctor

        public DataChangedEventArgs(IEnumerable<TEntity> items)
        {
            Entities = items?.ToArray() ?? new TEntity[0];
        }

        #endregion .ctor

        #region Properties

        public TEntity[] Entities { get; }

        #endregion Properties
    }
}