using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Domain
{
    public class Check : BaseEntity
    {
        public int RescheduleSecondInterval { get; set; }

        public Satellite Satellite { get; set; }
    }
}
