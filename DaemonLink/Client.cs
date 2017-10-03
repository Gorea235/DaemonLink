using System;

namespace DaemonLink
{
    public class Client
    {
        readonly int _port;

        public Client(int port)
        {
            _port = port;
        }

        public void Process()
        {
            using (DSocket.Connection conn = DSocket.Connection.ConnectTo(_port))
            {
                throw new NotImplementedException();
            }
        }
    }
}
