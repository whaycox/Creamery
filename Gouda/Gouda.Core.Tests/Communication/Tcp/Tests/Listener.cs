using System;
using System.Collections.Generic;
using System.Text;
using Curds;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Threading.Tasks;
using System.IO;

namespace Gouda.Communication.Tcp.Tests
{
    using Communication.Abstraction;
    using Communication.Domain;

    [TestClass]
    public class Listener : Test
    {
        private Implementation.Sender Sender = new Implementation.Sender();

        private async Task TestHandler(IConversation conversation)
        {
            await conversation.ReceiveObject();
            await conversation.Send(new Acknowledgement(MockTime.Fetch));
        }

        [TestMethod]
        public async Task ErrorResponseWithoutAHandler()
        {
            using (Implementation.Listener listener = new Implementation.Listener(TestEndpoint))
            {
                listener.Start();
                using (var conversation = await Sender.BeginConversation(TestEndpoint))
                {
                    await conversation.Send(new Acknowledgement(MockTime.Fetch));
                    Error error = await conversation.ReceiveObject() as Error;
                    Assert.AreEqual("System.InvalidOperationException: No handler has been provided", error.ExceptionText);                    
                }
            }
        }

        [TestMethod]
        public async Task ForwardsConversationToHandler()
        {
            using (Implementation.Listener listener = new Implementation.Listener(TestEndpoint))
            {
                listener.Handler = TestHandler;
                listener.Start();
                using (var conversation = await Sender.BeginConversation(TestEndpoint))
                {
                    await conversation.Send(new Acknowledgement(MockTime.Fetch));
                    Assert.IsInstanceOfType(await conversation.ReceiveObject(), typeof(Acknowledgement));
                }
            }
        }

        [TestMethod]
        public async Task CanListenOnArbitraryEndpoint()
        {
            using (Implementation.Listener listener = new Implementation.Listener(TestEndpoint))
            {
                listener.Handler = TestHandler;
                listener.Start();
                using (var conversation = await Sender.BeginConversation(TestEndpoint))
                {
                    await conversation.Send(new Acknowledgement(MockTime.Fetch));
                    Assert.IsInstanceOfType(await conversation.ReceiveObject(), typeof(Acknowledgement));
                }
            }
        }
        private static readonly IPEndPoint TestEndpoint = new IPEndPoint(IPAddress.Loopback, 43210);

    }
}
