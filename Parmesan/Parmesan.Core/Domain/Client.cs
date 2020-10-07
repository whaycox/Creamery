using System;
using System.Collections.Generic;
using System.Text;
using Curds.Persistence.Domain;

namespace Parmesan.Domain
{
    public class Client : SimpleEntity
    {
        public string PublicClientID { get; set; }
        public ClientType Type { get; set; }
    }
}
