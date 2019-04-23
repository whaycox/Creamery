using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Domain.Security
{
    public class MockSession : Session
    {
        public static Session One => new MockSession(MockUser.One.ID);
        public static Session Two => new MockSession(MockUser.Two.ID);
        public static Session Three => new MockSession(MockUser.Three.ID);

        public MockSession(int userID)
        {
            Identifier = NewSessionID;
            Series = ReAuth.NewSeries;
            UserID = userID;
            Expiration = DateTimeOffset.MinValue;
        }
    }
}
