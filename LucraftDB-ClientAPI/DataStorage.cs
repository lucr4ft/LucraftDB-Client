using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;

namespace Lucraft.Database.Client
{
    public class DataStorage
    {
        internal static readonly string Version = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;

        private const int DefaultPort = 7864;

        private static DataStorage _instance;
        private static Client _client;

        public static void SetInstance(string host)
        {
            _instance = new DataStorage(host, DefaultPort);
        }

        public static void SetInstance(string host, int port)
        {
            _instance = new DataStorage(host, port);
        }

        public static DataStorage GetInstance() => _instance;

        public static void Connect()
        {
            _client.Connect();
        }

        public static async Task ConnectAsync()
        {
            await _client.ConnectAsync();
        }

        private DataStorage(string host, int port)
        {
            _client = new Client(host, port);
        }

        public DatabaseReference GetDatabase(string id)
        {
            return new DatabaseReference(id);
        }

        internal static string MakeRequest(string req)
        {
            _client.Send(req);
            return _client.ReadLine();
        }
    }
}
