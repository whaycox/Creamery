using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Domain.Security
{
    public class MockAuthentication : Authentication
    {
        public MockAuthentication(int userID)
        {
            Session = new MockSession(userID);
            ReAuthentication = new MockReAuth(userID);
        }
    }
}
