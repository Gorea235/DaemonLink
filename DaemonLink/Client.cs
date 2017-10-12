using System;
using DaemonLink.DSocket;

namespace DaemonLink
{
    public class Client
    {
        readonly int _port;

        public Client(int port)
        {
            _port = port;
        }

        public int Process(string[] args)
        {
            using (Connection conn = Connection.ConnectTo(_port))
            {
                conn.SendMessage(MessageCode.Arguments, args.Length);
                foreach (string s in args)
                    conn.SendMessage(MessageCode.None, s);

                MessageCode code;
                while (true)
                {
                    code = conn.RecvMessage();
                    switch (code)
                    {
                        case MessageCode.Write:
                            Console.Out.Write(conn.RecvMessageChar());
                            break;
                        case MessageCode.Flush:
                            Console.Out.Flush();
                            break;
                        case MessageCode.WriteError:
                            Console.Error.Write(conn.RecvMessageChar());
                            break;
                        case MessageCode.FlushError:
                            Console.Error.Flush();
                            break;
                        case MessageCode.ReadChar:
                            conn.SendMessage(MessageCode.None, Console.In.Read());
                            break;
                        case MessageCode.Finished:
                            return conn.RecvMessageInt();
                        case MessageCode.Error:
                            Console.WriteLine("Daemon encountered an error while processing the command");
                            Console.WriteLine(conn.RecvMessageString());
                            return 1;
                        default:
                            Console.WriteLine("Daemon sent an unknown message");
                            return 1;
                    }
                }
            }
        }
    }
}
