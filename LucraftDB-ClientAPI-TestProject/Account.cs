using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
