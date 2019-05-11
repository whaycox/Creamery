using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Security.Domain
{
    public class Authentication
    {
        public Session Session { get; set; }
        public ReAuth ReAuthentication { get; set; }
    }
}
