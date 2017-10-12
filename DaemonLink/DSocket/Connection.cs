using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DaemonLink.DSocket
{
    class Connection : IDisposable
    {
        const string _msgDisposedEx = "Connection has be disposed";

        bool _disposed = false;
        readonly TcpClient _conn;

        internal Connection(TcpClient conn)
        {
            _conn = conn;
        }

        public static Connection ConnectTo(int port)
        {
            TcpClient conn = new TcpClient();
            conn.Connect(new IPEndPoint(IPAddress.Loopback, port));
            return new Connection(conn);
        }

        public void SendMessage(MessageCode code) => SendMessage(code, (byte[])null);

        public void SendMessage(MessageCode code, char c)
        {
            byte[] padded = { 0, 0, 0, 0 };
            Encoding.UTF8.GetBytes(new[] { c }).CopyTo(padded, 0);
            SendMessage(code, padded);
        }

        public void SendMessage(MessageCode code, string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            SendMessage(code, bytes.Length);
            SendMessage(MessageCode.None, bytes);
        }

        public void SendMessage(MessageCode code, int i) => SendMessage(code, BitConverter.GetBytes(i));

        public void SendMessage(MessageCode code, byte[] content)
        {
            _AssertDisposed();
            // for sending chars, use a 4 bit content and pad the rest
            // for arguments, send an int specifying the number of bytes
            // then send the rest of the content (this is fine unless the
            // arguments are 4.8GB in size, which i doubt)
            using (NetworkStream stream = _conn.GetStream())
            {
                if (code != MessageCode.None)
                    _WriteBytes(stream, new[] { (byte)code });
                if (code == MessageCode.Arguments)
                    _WriteBytes(stream, BitConverter.GetBytes(content.Length));
                if (content != null)
                    _WriteBytes(stream, content);
            }
        }

        public MessageCode RecvMessage() => (MessageCode)RecvMessageBytes(1)[0];

        public char RecvMessageChar() => Encoding.UTF8.GetChars(
            RecvMessageBytes(4))[0]; // chars are always 4 bytes long (as they are padded)

        public string RecvMessageString() => Encoding.UTF8.GetString(
            RecvMessageBytes( // receive the length of the string
                BitConverter.ToInt32(
                    RecvMessageBytes(4), 0))); // get the length of the string

        public int RecvMessageInt() => BitConverter.ToInt32(RecvMessageBytes(4), 0);

        public byte[] RecvMessageBytes(int n)
        {
            _AssertDisposed();
            using (NetworkStream stream = _conn.GetStream())
            {
                int read = 0;
                byte[] buffer = new byte[n];
                while (read < n)
                    read = stream.Read(buffer, read, n - read);
                return buffer;
            }
        }

        public void Dispose()
        {
            _conn.Dispose();
        }

        void _AssertDisposed()
        {
            if (_disposed) throw new ObjectDisposedException(_msgDisposedEx);
        }

        #region "Helper Methods"

        static void _WriteBytes(NetworkStream stream, byte[] bytes)
        {
            stream.Write(bytes, 0, bytes.Length);
        }

        #endregion
    }
}
