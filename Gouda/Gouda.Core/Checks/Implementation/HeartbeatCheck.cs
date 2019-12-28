using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Checks.Implementation
{
    using Gouda.Abstraction;

    public class HeartbeatCheck : ICheck
    {
        public Guid ID { get; } = Guid.Parse("b9e40947-cb83-446d-be2b-ff5f7b0dbc0c");
        public string Name => "Heartbeat";
    }
}
