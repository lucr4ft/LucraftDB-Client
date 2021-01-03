using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Lucraft.Database.Client
{
    class Client
    {
        private TcpClient tcpClient;
        private NetworkStream networkStream;

        public void Connect(string host, int port)
        {
            tcpClient = new TcpClient();
            tcpClient.Connect(host, port);
            networkStream = tcpClient.GetStream();
        }

        public void Send(string msg)
        {

        }

        public string ReadLine()
        {
            return "";
        }

        public void Disconnect()
        {
            Send("\u0004");
            tcpClient.Close();
        }
    }
}
