using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain.Enumerations;
using Gouda.Domain.Communication;
using Curds.Domain.Persistence;

namespace Gouda.Domain.Check
{
    public class MockDefinition : Definition
    {
        public const int SampleID = 1;
        public static MockDefinition Sample => new MockDefinition()
        {
            ID = SampleID,
            Name = nameof(MockDefinition),
            CheckID = MockCheck.SampleID,
            SatelliteID = MockSatellite.SampleID,
            ArgumentIDs = MockArgument.SampleIDs,
        };
    }
}
