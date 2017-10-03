using System;
using System.Net;
using System.Net.Sockets;

namespace DaemonLink.DSocket
{
    /// <summary>
    /// A simple socket wrapper class.
    /// This is specialised to handle the messages and byte data that the
    /// daemon server & client 
    /// </summary>
    class SServer : IDisposable
    {
        readonly Socket _server;

        public int Port => ((IPEndPoint)_server.LocalEndPoint).Port;

        public SServer(int port = 0)
        {
            _server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _server.Bind(new IPEndPoint(IPAddress.Loopback, port));
            _server.Listen(10);
        }

        public Connection Accept()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _server.Dispose();
        }
    }
}
