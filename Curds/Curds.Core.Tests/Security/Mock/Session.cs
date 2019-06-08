using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Security.Mock
{
    public class Session : Domain.Session
    {
        public static Domain.Session One => new Session(User.One.ID);
        public static Domain.Session Two => new Session(User.Two.ID);
        public static Domain.Session Three => new Session(User.Three.ID);

        public Session(int userID)
        {
            Identifier = nameof(Identifier);
            DeviceIdentifier = nameof(DeviceIdentifier);
            Series = nameof(Series);
            UserID = userID;
            ExtendExpiration(DateTimeOffset.MinValue);
        }
    }
}
