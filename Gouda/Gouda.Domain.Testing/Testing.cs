using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace Gouda
{
    using Domain.Communication;

    public partial class Testing
    {
        public const string AlwaysCronString = "* * * * *";
        public static IPEndPoint TestEndpoint => new IPEndPoint(IPAddress.Loopback, Satellite.DefaultPort);
    }
}
