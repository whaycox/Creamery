using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Gouda.Domain
{
    using Enumerations;

    public class Satellite : BaseNamedEntity
    {
        public IPAddress IPAddress { get; set; }
        public SatelliteStatus Status { get; set; }
    }
}
