using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Gouda.Domain.Check
{
    public class MockDefinitionArgument : DefinitionArgument
    {
        public static DefinitionArgument One => Sample(1);
        public static DefinitionArgument Two => Sample(2);
        public static DefinitionArgument Three => Sample(3);
        public static DefinitionArgument Four => Sample(4);

        public static DefinitionArgument[] Samples => new DefinitionArgument[]
        {
            One,
            Two,
            Three,
        };

        private static DefinitionArgument Sample(int val) => new DefinitionArgument
        {
            ID = val,
            DefinitionID = MockDefinition.SampleID,
            Name = val.ToString(),
            Value = val.ToString(),
        };
    }
}
