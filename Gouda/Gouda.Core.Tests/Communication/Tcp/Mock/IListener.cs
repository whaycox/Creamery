using Curds.Time.Abstraction;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Gouda.Communication.Tcp.Mock
{
    using Abstraction;
    using Communication.Domain;
    using Implementation;

    public class IListener : BaseListener
    {
        private ITime Time { get; }

        public int ExpectedObjects { get; set; } = 1;
        public bool ThrowOnHandle { get; set; } = false;

        public override ConversationHandler Handler
        {
            get => Handle;
            set => throw new NotImplementedException();
        }

        public IListener(ITime time)
            : base(Testing.TestEndpoint)
        {
            Time = time;
        }

        protected override Implementation.Conversation BuildConversation(TcpClient client) => new Conversation(client);

        public List<ICommunicableObject> ReceivedObjects = new List<ICommunicableObject>();
        private async Task Handle(IConversation conversation)
        {
            if (ThrowOnHandle)
                throw new Exception(nameof(ThrowOnHandle));

            for (int i = 0; i < ExpectedObjects; i++)
            {
                ReceivedObjects.Add(await conversation.ReceiveObject());
                await conversation.Send(new Acknowledgement(Time.Fetch));
            }
        }
    }
}
