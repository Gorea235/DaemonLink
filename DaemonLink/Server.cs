using System;
using System.Net;
using System.Net.Sockets;
using DaemonLink.DSocket;

namespace DaemonLink
{
    public class Server : IDisposable
    {
        readonly TcpListener _server;

        public int Port => ((IPEndPoint)_server.LocalEndpoint).Port;

        public event EventHandler<ParseEventArgs> ParseArguments;

        public event EventHandler Closed;

        public Server(int port = 0)
        {
            _server = new TcpListener(new IPEndPoint(IPAddress.Loopback, port));
        }

        public void Run()
        {
            _server.Start();
            while (true)
            {
                try
                {
                    using (Connection conn = new Connection(_server.AcceptTcpClient()))
                    {
                        if (conn.RecvMessage() == MessageCode.Arguments)
                        {
                            // get arguments
                            //  method: send int of number of arguments
                            //          then send the arguments seperately
                            int nargs = conn.RecvMessageInt();
                            string[] args = new string[nargs];
                            for (int i = 0; i < nargs; i++)
                                args[i] = conn.RecvMessageString();
                            
                            // setup redirect classes
                            DText.DStdOut nOut = new DText.DStdOut(conn);
                            DText.DStdIn nIn = new DText.DStdIn(conn);
                            DText.DStdErr nErr = new DText.DStdErr(conn);

                            // apply redirection
                            nOut.EnableRedirect();
                            nIn.EnableRedirect();
                            nErr.EnableRedirect();

                            // event args for status code changing
                            ParseEventArgs eargs = new ParseEventArgs(args);

                            // raise parse arguments event to process data
                            try
                            {
                                ParseArguments?.Invoke(this, eargs);
                            }
                            catch (Exception ex)
                            {
                                // an event handler threw, so tell the client
                                // and exit
                                conn.SendMessage(MessageCode.Error, ex.ToString());
                                throw ex;
                            }
                            finally
                            {
                                // cleanup redirection
                                nOut.DisableRedirect();
                                nIn.DisableRedirect();
                                nErr.DisableRedirect();
                            }

                            // tell the client that we're done and gives the
                            // exit code
                            conn.SendMessage(MessageCode.Finished, eargs.ExitStatus);
                        }
                    }
                }
                catch (SocketException ex)
                {
                    if (ex.SocketErrorCode == SocketError.Interrupted)
                        break;
                    throw ex;
                }
            }
            _server.Stop();
        }

        public void Dispose()
        {
            _server.Stop();
            Closed?.Invoke(this, new EventArgs());
        }
    }
}
