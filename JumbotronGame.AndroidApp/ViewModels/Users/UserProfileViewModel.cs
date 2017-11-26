using JumbotronGame.AndroidApp.Repositories;
using JumbotronGame.AndroidApp.ViewModels.Common;
using JumbotronGame.Server.DataContracts.Users;

namespace JumbotronGame.AndroidApp.ViewModels.Users
{
    public class UserProfileViewModel : SyncItemViewModel<UserProfile, UserProfileCriteria>
    {
        #region Fields

        private string _firstName;
        private string _lastName;
        private int _points;

        #endregion Fields

        #region .ctor

        public UserProfileViewModel(IRepository<UserProfile, UserProfileCriteria> repository)
            : base(repository)
        {
            StartReceiveNotifications();
        }

        #endregion .ctor

        #region Properties

        public string FirstName
        {
            get => _firstName;
            set
            {
                if (Set(ref _firstName, value))
                {
                    RaisePropertyChanged(nameof(FullName));
                }
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                if (Set(ref _lastName, value))
                {
                    RaisePropertyChanged(nameof(FullName));
                }
            }
        }

        public string FullName
        {
            get => $"{FirstName} {LastName}";
        }

        public int Points
        {
            get => _points;
            set => Set(ref _points, value);
        }

        #endregion Properties

        #region Methods

        protected override void Map(UserProfile entity)
        {
            Id = entity.Id;
            FirstName = entity.FirstName;
            LastName = entity.LastName;
            Points = entity.Points;
        }

        #endregion Methods
    }
}