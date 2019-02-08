using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain.Check;
using Gouda.Domain.Check.Responses;
using Gouda.Domain.Enumerations;

namespace Gouda.Check.Basic
{
    public class Heartbeat : BaseCheck
    {
        public override Guid ID => Guid.Parse("c97d3610-8b1e-4cc5-9a34-22325f19262f");

        public override Success Perform(Request request) => new Success();
        public override Status Evaluate(Success response) => Status.Good;
    }
}
