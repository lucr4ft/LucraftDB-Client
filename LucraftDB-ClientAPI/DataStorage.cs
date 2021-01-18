using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;

namespace Lucraft.Database.Client
{
    public class DataStorage
    {
        internal static readonly string Version = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;

        private static readonly int DefaultPort = 7864;

        private static DataStorage instance;
        private static Client client;

        public static void SetInstance(string host)
        {
            instance = new DataStorage(host, DefaultPort);
        }

        public static void SetInstance(string host, int port)
        {
            instance = new DataStorage(host, port);
        }

        public static DataStorage GetInstance() => instance;

        public static void Connect()
        {
            client.Connect();
        }

        public static async Task ConnectAsync()
        {
            await client.ConnectAsync();
        }

        private DataStorage(string host, int port)
        {
            client = new Client(host, port);
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
