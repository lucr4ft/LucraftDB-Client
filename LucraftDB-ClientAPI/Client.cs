using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Lucraft.Database.Client
{
    internal class Client
    {
        private TcpClient tcpClient;
        private StreamReader streamReader;
        private StreamWriter streamWriter;

        private readonly string host;
        private readonly int port;

        internal Client(string host, int port)
        {
            this.host = host;
            this.port = port;
        }

        internal async Task ConnectAsync()
        {
            tcpClient = new TcpClient();
            await tcpClient.ConnectAsync(host, port);
            streamReader = new StreamReader(tcpClient.GetStream());
            streamWriter = new StreamWriter(tcpClient.GetStream()) { AutoFlush = true };
            SendClientData();
        }

        internal void Connect()
        {
            tcpClient = new TcpClient();
            tcpClient.Connect(host, port);
            streamReader = new StreamReader(tcpClient.GetStream());
            streamWriter = new StreamWriter(tcpClient.GetStream()) { AutoFlush = true };
            SendClientData();
        }

        private void SendClientData()
        {
            //Send("version:{" + DataStorage.Version + "}");
        }

        internal void Send(string msg)
        {
            streamWriter.Write(msg + "\n");
        }

        internal async Task SendAsync(string msg)
        {
            await streamWriter.WriteAsync(msg + "\n");
        }

        internal string ReadLine()
        {
            return streamReader.ReadLine();
        }

        internal async Task<string> ReadAsync()
        {
            return await streamReader.ReadLineAsync();
        }

        internal void Disconnect()
        {
            streamReader.Close();
            streamWriter.Close();
            tcpClient.Close();
        }
    }
}
