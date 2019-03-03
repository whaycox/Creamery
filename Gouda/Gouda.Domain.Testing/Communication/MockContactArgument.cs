using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Domain.Communication
{
    public class MockContactArgument
    {
        public static ContactArgument One => new ContactArgument() { ID = 1, Name = nameof(One), Value = nameof(One), ContactID = MockContact.One.ID };
        public static ContactArgument Two => new ContactArgument() { ID = 2, Name = nameof(Two), Value = nameof(Two), ContactID = MockContact.Two.ID };
        public static ContactArgument Three => new ContactArgument() { ID = 3, Name = nameof(Three), Value = nameof(Three), ContactID = MockContact.Two.ID };
        public static ContactArgument Four => new ContactArgument() { ID = 4, Name = nameof(Four), Value = nameof(Four), ContactID = MockContact.One.ID };

        public static ContactArgument[] Samples => new ContactArgument[]
        {
            One,
            Two,
            Three,
            Four,
        };
    }
}
