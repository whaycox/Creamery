using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain.Enumerations;
using Gouda.Domain.Communication;

namespace Gouda.Domain.Check
{
    public class MockDefinition : Definition<MockResponse>
    {
        public const int MockID = 5;

        private static readonly string One = 1.ToString();
        private static readonly string Two = 2.ToString();
        private static readonly string Three = 3.ToString();

        public static readonly Dictionary<string, string> MockArguments = new Dictionary<string, string>()
        {
            { nameof(One), One },
            { nameof(Two), Two },
            { nameof(Three), Three },
        };
        
        public override Dictionary<string, string> Arguments => MockArguments;

        public MockDefinition(Satellite satellite)
            : base(satellite)
        {
            Name = nameof(MockDefinition);
            ID = MockID;
        }

        public override MockResponse BuildResponse(Response response) => new MockResponse(response);

        protected override Status Evaluate(MockResponse response)
        {
            if (response.CountData == 4)
                return Status.Good;
            else
                return Status.Worried;
        }
    }
}
