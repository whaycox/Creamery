using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Application.Security.Command.Domain
{
    using Application.Domain;

    public class Session : BaseViewModel
    {
        public string Identifier { get; set; }
        public string DeviceIdentifier { get; set; }

        public int UserID { get; set; }

        public DateTimeOffset Created { get; set; }
        public DateTimeOffset WouldExpire { get; set; }
    }
}
