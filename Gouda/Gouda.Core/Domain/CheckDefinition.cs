using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Domain
{
    public class CheckDefinition : BaseEntity
    {
        public Guid CheckID { get; set; }
        public int SatelliteID { get; set; }
        public string Name { get; set; }
        public int? RescheduleSecondInterval { get; set; }

        public Satellite Satellite { get; set; }
    }
}
