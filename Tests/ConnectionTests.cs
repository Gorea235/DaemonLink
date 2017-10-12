using System;
using System.Threading;
using DaemonLink;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class ConnectionTests
    {
        Thread servThread;

        [TestCleanup]
        public void CleanUp()
        {
            if (servThread != null && servThread.IsAlive)
                servThread.Abort();
        }

        [TestMethod]
        [Timeout(500)]
        public void TestSetPortConnection()
        {
            Server serv = new Server(5000);
            StartServerThread(serv);
            Client client = new Client(5000);
            Assert.AreEqual(0, client.Process(new string[] { }));
            serv.Dispose();
        }

        void StartServerThread(Server serv)
        {
            servThread = new Thread(serv.Run);
            servThread.Start();
        }
    }
}
