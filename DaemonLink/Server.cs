using System;

namespace DaemonLink
{
    public class Server : IDisposable
    {
        readonly DSocket.SServer _server;

        public event EventHandler<ParseEventArgs> ParseArguments;

        public event EventHandler Closed;

        public Server(int port = 0)
        {
            _server = new DSocket.SServer(port);
        }

        public void Dispose()
        {
            _server.Dispose();
            Closed?.Invoke(this, new EventArgs());
        }
    }
}
