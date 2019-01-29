using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Domain.Check
{
    public class MockResponse : Response
    {
        private const int Count = 4;
        private const decimal Decimal = 3.5463211m;

        private static readonly Dictionary<string, string> MockData = new Dictionary<string, string>()
        {
            { nameof(Count), Count.ToString() },
            { nameof(Decimal), Decimal.ToString() },
        };

        public int CountData => int.Parse(Arguments[nameof(Count)]);
        public decimal RealData => decimal.Parse(Arguments[nameof(Decimal)]);

        public MockResponse(Response basicResponse)
            : base(basicResponse)
        { }

        public MockResponse()
            : base(MockData)
        { }
    }
}
