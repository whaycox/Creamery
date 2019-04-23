using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Domain.Security
{
    public class Authentication
    {
        public Session Session { get; set; }
        public ReAuth ReAuthentication { get; set; }
    }
}
