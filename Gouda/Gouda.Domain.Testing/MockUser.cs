using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Domain
{
    public class MockUser : User
    {
        public static MockUser One => new MockUser(1);
        public static MockUser Two => new MockUser(2);
        public static MockUser Three => new MockUser(3);

        private MockUser(int id)
        {
            ID = id;
            Name = id.ToString();
        }
    }
}
