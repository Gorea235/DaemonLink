using System;
using System.IO;
using DaemonLink.DSocket;

namespace DaemonLink.DText
{
	/// <summary>
	/// A TextReader that can replace StdIn in order to receive input from the
    /// client, rather than blocking the daemon process.
	/// </summary>
	class DStdIn : TextReader
    {
        readonly TextReader _defReader;
        readonly Connection _conn;

        public DStdIn(Connection conn)
        {
            _conn = conn;
            _defReader = Console.In;
		}

		public void EnableRedirect()
		{
			Console.SetIn(this);
		}

		public void DisableRedirect()
		{
			Console.SetIn(_defReader);
		}

        public override int Read()
        {
            _conn.SendMessage(MessageCode.ReadChar);
            return _conn.RecvMessageInt();
        }
    }
}
