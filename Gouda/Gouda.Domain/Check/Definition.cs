using Curds.Domain;
using Curds.Domain.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gouda.Domain.Check
{
    using Enumerations;

    public class Definition : NamedEntity
    {
        public Status Status { get; set; }
        public int SatelliteID { get; set; }
        public List<int> ArgumentIDs { get; set; }
        public Guid CheckID { get; set; }
        public TimeSpan RescheduleSpan { get; set; }

        public Definition()
        {
            ArgumentIDs = new List<int>();
        }
    }
}
