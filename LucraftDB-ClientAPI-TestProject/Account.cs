namespace Lucraft.Database.Client.Test
{
    public class Account
    {
        [DatabaseProperty("username")]
        public string Username { get; init; }
        [DatabaseProperty("email")]
        public string Email { get; init; }
        [DatabaseProperty("password-hash")]
        public string PasswordHash { get; init; }
        [DatabaseProperty("verified")]
        public bool Verified { get; init; }

        public override string ToString()
        {
            return "username: " + Username + ";email: " + Email + ";password-hash: " + PasswordHash;
        }
    }
}
