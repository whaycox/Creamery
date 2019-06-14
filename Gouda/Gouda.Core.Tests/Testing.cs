using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Gouda
{
    using Communication.Tcp.Implementation;

    public static class Testing
    {
        public static IPEndPoint TestEndpoint = new IPEndPoint(IPAddress.Loopback, Listener.DefaultPort);
    }
}
