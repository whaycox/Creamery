using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Gouda.Communication.Tcp.Implementation
{
    using Abstraction;

    public class Listener : BaseListener
    {
        public const int DefaultPort = 9326;

        public override ConversationHandler Handler { get; set; }

        public Listener(IPAddress address)
            : base(new IPEndPoint(address, DefaultPort))
        { }

        public Listener(IPEndPoint endpoint)
            : base(endpoint)
        { }
    }
}
