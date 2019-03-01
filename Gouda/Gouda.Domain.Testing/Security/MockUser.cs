using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Domain.Security
{
    public class MockUser : User
    {
        public static User One => Sample(1);
        public static User Two => Sample(2);
        public static User Three => Sample(3);

        public static User[] Samples => new User[]
        {
            One,
            Two,
            Three,
        };

        private static User Sample(int id) => new User
        {
            ID = id,
            Name = id.ToString(),
            Email = Testing.TestEmail,
            Password = Curds.Testing.TestPassword,
            Salt = Curds.Testing.TestSalt,
        };
    }
}
