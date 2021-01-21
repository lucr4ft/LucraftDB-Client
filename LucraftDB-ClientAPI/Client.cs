using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Lucraft.Database.Client
{
    internal class Client
    {
        private TcpClient _tcpClient;
        private StreamReader _streamReader;
        private StreamWriter _streamWriter;

        private readonly string _host;
        private readonly int _port;

        internal Client(string host, int port)
        {
            _host = host;
            _port = port;
        }

        internal async Task ConnectAsync()
        {
            _tcpClient = new TcpClient();
            await _tcpClient.ConnectAsync(_host, _port);
            _streamReader = new StreamReader(_tcpClient.GetStream());
            _streamWriter = new StreamWriter(_tcpClient.GetStream()) { AutoFlush = true };
            //SendClientData();
        }

        internal void Connect()
        {
            _tcpClient = new TcpClient();
            _tcpClient.Connect(_host, _port);
            _streamReader = new StreamReader(_tcpClient.GetStream());
            _streamWriter = new StreamWriter(_tcpClient.GetStream()) { AutoFlush = true };
            //SendClientData();
        }

        private void SendClientData()
        {
            //Send("version:{" + DataStorage.Version + "}");
        }

        internal void Send(string msg)
        {
            _streamWriter.Write(msg + "\n");
        }

        internal async Task SendAsync(string msg)
        {
            await _streamWriter.WriteAsync(msg + "\n");
        }

        internal string ReadLine()
        {
            return _streamReader.ReadLine();
        }

        internal async Task<string> ReadAsync()
        {
            return await _streamReader.ReadLineAsync();
        }

        internal void Disconnect()
        {
            _streamReader.Close();
            _streamWriter.Close();
            _tcpClient.Close();
        }
    }
}
