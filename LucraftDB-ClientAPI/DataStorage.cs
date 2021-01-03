using System;
using System.Collections.Generic;
using System.Text;

namespace Lucraft.Database.Client
{
    public class DataStorage
    {
        private static readonly int DefaultPort = 7864;
        public static DataStorage Instance;

        public static void SetInstance(string host)
        {
            Instance = new DataStorage(host, DefaultPort);
        }

        public static void SetInstance(string host, int port)
        {
            Instance = new DataStorage(host, port);
        }

        private readonly Client client;

        private DataStorage(string host, int port)
        {
            client = new Client();
            client.Connect(host, port);
        }

        public DatabaseReference GetDatabase(string id)
        {
            return new DatabaseReference(id);
        }
    }
}
