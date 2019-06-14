using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Gouda.Communication.Tcp.Tests
{
    using Communication.Abstraction;
    using Communication.Domain;

    [TestClass]
    public class Conversation : Template.IListener
    {
        [TestMethod]
        public async Task CanSendAndReceiveAcknowledgement()
        {
            using (var conversation = BuildTestConversation())
            {
                Acknowledgement toSend = new Acknowledgement(MockTime.Fetch);
                await conversation.Send(toSend);
                ICommunicableObject received = await conversation.ReceiveObject();
                VerifyAcknowledgement(toSend.Time, received);
            }
        }
        private void VerifyAcknowledgement(DateTimeOffset expectedTime, ICommunicableObject received)
        {
            Assert.IsInstanceOfType(received, typeof(Acknowledgement));
            Assert.AreEqual(expectedTime, (received as Acknowledgement).Time);
        }

        [TestMethod]
        public async Task CanSendMultipleObjects()
        {
            MockListener.ExpectedObjects = 3;
            using (var conversation = BuildTestConversation())
            {
                await conversation.Send(new Acknowledgement(MockTime.Fetch));
                ICommunicableObject received = await conversation.ReceiveObject();
                VerifyAcknowledgement(MockTime.Fetch, received);

                await conversation.Send(new Communication.Mock.ICommunicableObject());
                received = await conversation.ReceiveObject();
                VerifyAcknowledgement(MockTime.Fetch, received);

                MockTime.SetPointInTime(DateTimeOffset.MaxValue);
                await conversation.Send(new Acknowledgement(MockTime.Fetch));
                received = await conversation.ReceiveObject();
                VerifyAcknowledgement(MockTime.Fetch, received);
            }
        }
    }
}
