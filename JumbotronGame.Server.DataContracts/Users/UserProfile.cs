namespace JumbotronGame.Server.DataContracts.Users
{
    public class UserProfile : Entity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Points { get; set; }
    }
}