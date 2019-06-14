using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Gouda.Communication.Tcp.Tests
{
    using Abstraction;
    using Communication.Domain;

    [TestClass]
    public class Sender : Template.IListener<Implementation.Sender>
    {
        private ICommunicableObject MockCommunicableObject => new Communication.Mock.ICommunicableObject();

        protected override Implementation.Sender TestObject => new Implementation.Sender();

        [TestMethod]
        public async Task CanBeginConversation()
        {
            using (IConversation conversation = await TestObject.BeginConversation(Testing.TestEndpoint))
            { }
        }

        [TestMethod]
        public async Task CanSendObjectAndReceiveAcknowledgement()
        {
            using (IConversation conversation = await TestObject.BeginConversation(Testing.TestEndpoint))
            {
                await conversation.Send(MockCommunicableObject);
                ICommunicableObject received = await conversation.ReceiveObject();
                Assert.IsInstanceOfType(received, typeof(Acknowledgement));
            }
        }
    }
}
