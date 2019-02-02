using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Domain.Check
{
    public class MockArgument : Argument
    {
        public static Argument One => new MockArgument(1);
        public static Argument Two => new MockArgument(2);
        public static Argument Three => new MockArgument(3);
        public static Argument Four => new MockArgument(4);

        public static List<Argument> MockArguments => new List<Argument>()
        {
            One,
            Two,
            Three,
        };

        private MockArgument(int val)
        {
            ID = val;
            Name = val.ToString();
            Value = val.ToString();
        }
    }
}
