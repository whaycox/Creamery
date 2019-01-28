using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Domain
{
    public class AcceptanceCase
    {
        public Action Delegate { get; set; }
        public bool ShouldSucceed { get; set; }
    }
}
