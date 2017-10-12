using System;
using System.Threading;
using DaemonLink;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    //[TestClass]
    public class CommunicationTests
    {
        int port;
        Server serv;
        Thread servThread;

        [TestInitialize]
        public void Init()
        {
            serv = new Server();
            port = serv.Port;
            servThread = new Thread(serv.Run);
            servThread.Start();
        }

        [TestCleanup]
        public void CleanUp()
        {
            serv.Dispose();
        }
    }
}
