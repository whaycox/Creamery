using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Domain.Security
{
    public class MockReAuth : ReAuth
    {
        public static ReAuth One => new MockReAuth(MockUser.One.ID);
        public static ReAuth Two => new MockReAuth(MockUser.Two.ID);
        public static ReAuth Three => new MockReAuth(MockUser.Three.ID);

        public MockReAuth(int userID)
        {
            DeviceIdentifier = Testing.DeviceIdentifier;
            UserID = userID;
            Token = NewToken;
            Series = NewSeries;
        }
    }
}
