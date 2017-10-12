using System;
using System.IO;
using System.Text;
using DaemonLink.DSocket;

namespace DaemonLink.DText
{
    /// <summary>
    /// Mimics the DStdOut class, however it uses the <code>Console.Error</code>
    /// standard stream, rather than <code>Console.Out</code>.
    /// </summary>
    class DStdErr : TextWriter
    {
        readonly TextWriter _defWriter;
        readonly Connection _conn;

        public bool AutoFlush => true;

        public DStdErr(Connection conn)
        {
            _conn = conn;
            _defWriter = Console.Error;
        }

        public void EnableRedirect()
        {
            Console.SetError(this);
        }

        public void DisableRedirect()
        {
            Console.SetError(_defWriter);
        }

        public override Encoding Encoding => _defWriter.Encoding;

        public override void Write(char value)
        {
            _conn.SendMessage(MessageCode.WriteError, value);
            if (AutoFlush)
                Flush();
        }

        public override void Flush()
        {
            _conn.SendMessage(MessageCode.FlushError);
        }
    }
}
