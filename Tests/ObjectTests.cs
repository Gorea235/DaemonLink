using System;
using System.Threading;
using DaemonLink;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class ObjectTests
    {
        //[Ignore]
        [TestMethod]
        public void TestServerCtor()
        {
            Server serv = new Server();
            Assert.IsTrue(serv.Port > 0);
            serv.Dispose();

            serv = new Server(1234);
            Assert.AreEqual(1234, serv.Port);
            serv.Dispose();

            serv = new Server(5432);
            Assert.AreEqual(5432, serv.Port);
            serv.Dispose();
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void TestServerDtor1()
        {
            Server serv = new Server();
            serv.Dispose();
            serv.Run();
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void TestServerDtor2()
        {
            Server serv = new Server(1234);
            serv.Dispose();
            serv.Run();
        }

        [Ignore]
        [TestMethod]
        [Timeout(500)]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void TestServerDtor3()
        {
            Server serv = new Server();
            Thread servThread = new Thread(serv.Run);
            servThread.Start();
            serv.Dispose();
            Assert.IsFalse(servThread.IsAlive);
        }

        [TestMethod]
        public void TestClientCtor()
        {
            Client client = new Client(1235);
            client = new Client(5432);
        }
    }
}
