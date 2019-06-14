using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System;

namespace Gouda.Communication.Tcp.Tests
{
    using Communication.Abstraction;
    using Communication.Domain;

    [TestClass]
    public class BaseListener : Template.IListener
    {
        [TestMethod]
        public void CantBuildConversationWhenStopped()
        {
            MockListener.Stop();
            try
            {
                BuildTestConversation();
                Assert.Fail();
            }
            catch (Exception)
            { }
        }

        [TestMethod]
        public async Task CanRestartAfterStopping()
        {
            CantBuildConversationWhenStopped();
            MockListener.Start();
            using (var conversation = BuildTestConversation())
            {
                await conversation.Send(new Communication.Mock.ICommunicableObject());
                await conversation.ReceiveObject();
            }
            Assert.AreEqual(1, MockListener.ReceivedObjects.Count);
        }

        [TestMethod]
        public async Task SendsErrorIfHandlerThrows()
        {
            MockListener.ThrowOnHandle = true;
            MockListener.Start();
            using (var conversation = BuildTestConversation())
            {
                await conversation.Send(new Communication.Mock.ICommunicableObject());
                ICommunicableObject error = await conversation.ReceiveObject();
                Assert.IsInstanceOfType(error, typeof(Error));
            }
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(5)]
        [DataRow(7)]
        [DataRow(9)]
        [DataRow(15)]
        public async Task CanReceiveNObjects(int numberToSend)
        {
            MockListener.ExpectedObjects = numberToSend;
            using (var conversation = BuildTestConversation())
            {
                for (int i = 0; i < numberToSend; i++)
                {
                    await conversation.Send(new Communication.Mock.ICommunicableObject());
                    await conversation.ReceiveObject();
                }
            }
            Assert.AreEqual(numberToSend, MockListener.ReceivedObjects.Count);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(5)]
        [DataRow(7)]
        [DataRow(9)]
        [DataRow(15)]
        public async Task CanEstablishNConversations(int numberToEstablish)
        {
            for (int i = 0; i < numberToEstablish; i++)
            {
                using (var conversation = BuildTestConversation())
                {
                    await conversation.Send(new Communication.Mock.ICommunicableObject());
                    await conversation.ReceiveObject();
                }
            }
            Assert.AreEqual(numberToEstablish, MockListener.ReceivedObjects.Count);
        }

        [TestMethod]
        public async Task CanEstablishMultipleConversationsWithExceptions()
        {
            MockListener.ThrowOnHandle = true;
            for (int i = 0; i < 5; i++)
            {
                using (var conversation = BuildTestConversation())
                {
                    await conversation.Send(new Communication.Mock.ICommunicableObject());
                    await conversation.ReceiveObject();
                }
            }
            Assert.AreEqual(0, MockListener.ReceivedObjects.Count);
        }
    }
}
