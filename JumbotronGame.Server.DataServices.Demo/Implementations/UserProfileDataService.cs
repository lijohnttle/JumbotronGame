using JumbotronGame.Server.DataContracts.Users;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace JumbotronGame.Server.DataServices.Demo.Implementations
{
    public class UserProfileDataService : DataServiceBase<UserProfile, UserProfileCriteria>
    {
        #region Fields

        private readonly UserProfile _userProfile;

        #endregion Fields

        #region .ctor

        public UserProfileDataService()
        {
            _userProfile = new UserProfile
            {
                Id = 0,
                FirstName = "John",
                LastName = "Doe",
                Points = 1326
            };
        }

        #endregion .ctor

        #region Methods

        public override async Task<UserProfile> GetItemAsync(int id, CancellationToken ct)
        {
            return await Task.FromResult(_userProfile).ConfigureAwait(false);
        }

        public override async Task<IEnumerable<UserProfile>> GetItemsAsync(UserProfileCriteria criteria, CancellationToken ct)
        {
            return await Task.FromResult(new [] { _userProfile }).ConfigureAwait(false);
        }

        public override Task<UserProfile> SaveItemAsync(UserProfile entity, CancellationToken ct)
        {
            throw new System.NotImplementedException();
        }

        #endregion Methods
    }
}