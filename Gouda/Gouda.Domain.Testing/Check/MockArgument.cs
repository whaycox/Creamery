using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Gouda.Domain.Check
{
    public class MockArgument : Argument
    {
        public static Argument One => new MockArgument(1);
        public static Argument Two => new MockArgument(2);
        public static Argument Three => new MockArgument(3);
        public static Argument Four => new MockArgument(4);

        public static List<Argument> Samples => new List<Argument>()
        {
            One,
            Two,
            Three,
        };
        public static List<int> SampleIDs => Samples.Select(a => a.ID).ToList();

        private MockArgument(int val)
        {
            ID = val;
            DefinitionID = MockDefinition.SampleID;

            Name = val.ToString();
            Value = val.ToString();
        }
    }
}
