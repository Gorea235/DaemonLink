using System;
using System.Net.Sockets;

namespace DaemonLink.DSocket
{
    class Connection : IDisposable
    {
        readonly Socket _sock;

        public Connection(Socket sock)
        {
            _sock = sock;
        }

        public static Connection ConnectTo(int port)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _sock.Dispose();
        }
    }
}
