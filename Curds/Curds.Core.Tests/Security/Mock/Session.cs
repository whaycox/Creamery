using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Security.Mock
{
    public class Session : Domain.Session
    {
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
