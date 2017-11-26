using JumbotronGame.AndroidApp.ViewModels.Common;
using System;

namespace JumbotronGame.AndroidApp.ViewModels.ArenaEvents
{
    public class ArenaEventListItemViewModel : ExtendedViewModel, IItemViewModel
    {
        #region Fields

        private int _id;
        private string _header;
        private DateTime _date;

        #endregion Fields

        #region Properties

        public int Id
        {
            get => _id;
            set => Set(ref _id, value);
        }

        public string Header
        {
            get => _header;
            set => Set(ref _header, value);
        }

        public DateTime Date
        {
            get => _date;
            set => Set(ref _date, value);
        }

        #endregion Properties
    }
}