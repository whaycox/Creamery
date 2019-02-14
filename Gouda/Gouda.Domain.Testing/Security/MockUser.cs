using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Domain.Security
{
    public class MockUser : User
    {
        public static MockUser One => new MockUser(1);
        public static MockUser Two => new MockUser(2);
        public static MockUser Three => new MockUser(3);

        public static IEnumerable<User> Samples => new List<User>()
        {
            One,
            Two,
            Three,
        };

        private MockUser(int id)
        {
            ID = id;
            Name = id.ToString();
            Email = Testing.TestEmail;
            Password = Curds.Testing.TestPassword;
            Salt = Curds.Testing.TestSalt;
        }
    }
}
