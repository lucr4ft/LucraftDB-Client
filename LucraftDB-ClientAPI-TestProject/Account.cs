namespace Lucraft.Database.Client.Test
{
    public class Account
    {
        [DatabaseProperty("username")]
        public string Username { get; set; }
        [DatabaseProperty("email")]
        public string Email { get; set; }
        [DatabaseProperty("password-hash")]
        public string PasswordHash { get; set; }

        public override string ToString()
        {
            return "username: " + Username + ";email: " + Email + ";password-hash: " + PasswordHash;
        }
    }
}
