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
        public static bool ShouldFail { get; set; }

        public static readonly Guid SampleID = Guid.Parse("30d7e69b-f611-447f-a6ae-88887caa870d");

        public override Guid ID => SampleID;

        public override Status Evaluate(Success response) => ShouldFail ? Status.Critical : Status.Good;
        public override Success Perform(Request request) => ShouldFail ? throw new Exception($"{nameof(ShouldFail)} is {ShouldFail}") : new MockResponse();
    }
}
