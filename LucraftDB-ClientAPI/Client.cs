using System.IO;
using System.Net.Sockets;

namespace Lucraft.Database.Client
{
    class Client
    {
        private TcpClient tcpClient;
        private StreamReader streamReader;
        private StreamWriter streamWriter;

        public void Connect(string host, int port)
        {
            tcpClient = new TcpClient();
            tcpClient.Connect(host, port);
            streamReader = new StreamReader(tcpClient.GetStream());
            streamWriter = new StreamWriter(tcpClient.GetStream())
            {
                AutoFlush = true
            };
        }

        public void Send(string msg)
        {
            streamWriter.Write(msg + "\n");
        }

        public string ReadLine()
        {
            return streamReader.ReadLine();
        }

        public void Disconnect()
        {
            streamReader.Close();
            streamWriter.Close();
            tcpClient.Close();
        }
    }
}
