using Curds.Domain;
using Curds.Domain.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gouda.Domain.Check
{
    using Enumerations;
    using Communication;

    public class Definition : NamedEntity
    {
        public Status Status { get; set; }
        public Guid CheckGuid { get; set; }
        public TimeSpan RescheduleSpan { get; set; }

        public int SatelliteID { get; set; }
        public Satellite Satellite { get; set; }

        public IEnumerable<DefinitionArgument> DefinitionArguments { get; set; }
    }
}
