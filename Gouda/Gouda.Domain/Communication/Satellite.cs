using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using Curds.Domain.Persistence;
using Curds.Domain;

namespace Gouda.Domain.Communication
{
    using Check;

    public class Satellite : NamedEntity
    {
        public const int DefaultPort = 9326;

        public IPEndPoint Endpoint { get; set; }
    }
}
