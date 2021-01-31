using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
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

        private readonly string _host;
        private readonly int _port;

        internal Client(string host, int port)
        {
            _host = host;
            _port = port;
        }

        internal async Task ConnectAsync()
        {
            tcpClient = new TcpClient();
            await tcpClient.ConnectAsync(_host, _port);
            streamReader = new StreamReader(tcpClient.GetStream());
            streamWriter = new StreamWriter(tcpClient.GetStream()) { AutoFlush = true };
            SendClientData();
        }

        internal void Connect()
        {
            tcpClient = new TcpClient();
            tcpClient.Connect(_host, _port);
            streamReader = new StreamReader(tcpClient.GetStream());
            streamWriter = new StreamWriter(tcpClient.GetStream()) { AutoFlush = true };
            SendClientData();
        }

        private void SendClientData()
        {
            Send("version=" + DataStorage.Version);
            string response = ReadLine();
            var model = JsonConvert.DeserializeObject<IDictionary<string, string>>(response);
            if (model.ContainsKey("error"))
            {
                if (model["error"].Equals("lucraft.database.exception.outdated_client"))
                    throw new OutdatedClientException(model["error-message"]);
            }
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

        internal async Task<string> ReadLineAsync()
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
