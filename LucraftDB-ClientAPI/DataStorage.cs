using NuGet;

namespace Lucraft.Database.Client
{
    public class DataStorage
    {
        static internal string Version = new SemanticVersion(1, 0, 2, "beta.1").ToNormalizedString();

        private static readonly int DefaultPort = 7864;
        private static readonly Client client = new();
        public static DataStorage Instance;

        public static void SetInstance(string host)
        {
            Instance = new DataStorage(host, DefaultPort);
        }

        public static void SetInstance(string host, int port)
        {
            Instance = new DataStorage(host, port);
        }

        private DataStorage(string host, int port)
        {
            client.Connect(host, port);
        }

        public DatabaseReference GetDatabase(string id)
        {
            return new DatabaseReference(id);
        }

        static internal string MakeRequest(string req)
        {
            client.Send(req);
            return client.ReadLine();
        }
    }
}
