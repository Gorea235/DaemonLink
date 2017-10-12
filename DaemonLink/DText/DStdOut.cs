using System;
using System.IO;
using System.Text;
using DaemonLink.DSocket;

namespace DaemonLink.DText
{
    /// <summary>
    /// A TextWriter that can replace StdOut in order to send the messages
    /// to the client over the connection, instead of to the normal console.
    /// </summary>
    class DStdOut : TextWriter
    {
        readonly TextWriter _defWriter;
        readonly Connection _conn;

        public bool AutoFlush => true;

        public DStdOut(Connection conn)
        {
            _conn = conn;
            _defWriter = Console.Out;
        }

        public void EnableRedirect()
        {
            Console.SetOut(this);
        }

        public void DisableRedirect()
        {
            Console.SetOut(_defWriter);
        }

        public override Encoding Encoding => _defWriter.Encoding;

        public override void Write(char value)
        {
            _conn.SendMessage(MessageCode.Write, value);
            if (AutoFlush)
                Flush();
        }

        public override void Flush()
        {
            _conn.SendMessage(MessageCode.Flush);
        }
    }
}
