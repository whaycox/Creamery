using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Checks.Implementation
{
    using Gouda.Abstraction;

    internal class HeartbeatCheck : ICheck
    {
        private static readonly Guid _id = Guid.Parse("b9e40947-cb83-446d-be2b-ff5f7b0dbc0c");

        public Guid ID => _id;
        public string Name => "Heartbeat";
    }
}
