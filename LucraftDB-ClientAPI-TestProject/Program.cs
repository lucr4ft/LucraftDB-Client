using System;

namespace Lucraft.Database.Client.Test
{
    class Program
    {
        static void Main()
        {
            DataStorage.SetInstance("lucraft.ddns.net");

            _ = DataStorage.Instance.GetDatabase("test").GetCollection("test3").Query<Account>(
                (acc) => acc.Email != "luca.lewin23@gmail.com" || acc.Username != "Luca Lewin" || acc.PasswordHash == acc.Username);
        }
    }
}
