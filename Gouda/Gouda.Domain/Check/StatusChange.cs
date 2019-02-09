using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain.Enumerations;

namespace Gouda.Domain.Check
{
    public class StatusChange
    {
        public Definition Definition { get; set; }
        public Status Old { get; set; }
        public Status New { get; set; }
        public BaseResponse Response { get; set; }
    }
}
