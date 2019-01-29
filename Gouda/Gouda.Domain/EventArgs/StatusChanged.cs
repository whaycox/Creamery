using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Domain.EventArgs
{
    using Enumerations;
    using Check;

    public class StatusChanged : System.EventArgs
    {
        public Definition Definition { get; }
        public Status Old { get; }
        public Status New { get; }

        public StatusChanged(Definition definition, Status oldStatus, Status newStatus)
        {
            Definition = definition;
            Old = oldStatus;
            New = newStatus;
        }
    }
}
