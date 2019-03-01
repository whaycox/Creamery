﻿using System;
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
        public static Definition Sample => new Definition()
        {
            ID = SampleID,
            Name = nameof(MockDefinition),
            CheckGuid = MockCheck.SampleID,
            SatelliteID = MockSatellite.SampleID,
            RescheduleSpan = TimeSpan.FromTicks(100),
        };
    }
}
