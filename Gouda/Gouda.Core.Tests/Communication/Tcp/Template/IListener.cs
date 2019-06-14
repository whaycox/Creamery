using Curds;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Sockets;

namespace Gouda.Communication.Tcp.Template
{
    public abstract class IListener : Test
    {
        protected Mock.IListener MockListener = null;

        protected Implementation.Conversation BuildTestConversation() =>
            new Implementation.Conversation(
                new TcpClient(
                    Testing.TestEndpoint.Address.ToString(),
                    Testing.TestEndpoint.Port));

        [TestInitialize]
        public void BuildListener()
        {
            MockListener = new Mock.IListener(MockTime);
            MockListener.Start();
        }

        [TestCleanup]
        public void Dispose()
        {
            MockListener.Dispose();
        }
    }

    public abstract class IListener<T> : IListener
    {
        protected abstract T TestObject { get; }
    }
}
