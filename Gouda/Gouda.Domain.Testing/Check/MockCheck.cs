using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Check;
using Gouda.Domain.Check.Responses;
using Gouda.Domain.Enumerations;

namespace Gouda.Domain.Check
{
    public class MockCheck : BaseCheck
    {
        public static Guid SampleID => Guid.Empty;

        public override Guid ID => SampleID;

        public override Status Evaluate(Success response) => Status.Good;
        public override Success Perform(Request request) => new MockResponse();
    }
}
