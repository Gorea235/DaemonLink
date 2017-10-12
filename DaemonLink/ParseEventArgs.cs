using System;

namespace DaemonLink
{
    public class ParseEventArgs : EventArgs
    {
        public string[] Args { get; private set; }
        public int ExitStatus { get; set; } = 0;

        public ParseEventArgs(string[] args)
        {
            Args = args;
        }
    }
}
