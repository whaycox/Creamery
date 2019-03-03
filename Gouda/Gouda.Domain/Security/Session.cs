using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Domain.Security
{
    public class Session
    {
        public string Identifier { get; set; }
        public string DeviceIdentifier { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }

        public DateTimeOffset Created { get; set; }
        public DateTimeOffset WouldExpire { get; set; }
    }
}
