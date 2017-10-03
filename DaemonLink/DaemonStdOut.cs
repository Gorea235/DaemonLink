using System;
using System.IO;
using System.Text;

namespace DaemonLink
{
    public class DaemonStdOut : TextWriter
    {
        readonly TextWriter _stdOut;

        public DaemonStdOut()
        {
            _stdOut = Console.Out;
        }

        public void EnableRedirect()
        {
            Console.SetOut(this);
        }

        public void DisableRedirect()
        {
            Console.SetOut(_stdOut);
        }

        public override Encoding Encoding => _stdOut.Encoding;

        public override void Write(char value)
        {
            throw new NotImplementedException();
        }
    }
}
