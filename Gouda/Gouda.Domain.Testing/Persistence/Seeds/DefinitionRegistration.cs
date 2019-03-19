using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Domain.Persistence.Seeds
{
    using Check;
    using Security;

    public static class DefinitionRegistration
    {
        public static Check.DefinitionRegistration[] Data => new Check.DefinitionRegistration[]
        {
            new Check.DefinitionRegistration() { ID = 1, DefinitionID = MockDefinition.SampleID, UserID = MockUser.One.ID, CronString = Testing.AlwaysCronString },
            new Check.DefinitionRegistration() { ID = 2, DefinitionID = MockDefinition.SampleID, UserID = MockUser.Two.ID, CronString = Testing.AlwaysCronString },
            new Check.DefinitionRegistration() { ID = 3, DefinitionID = MockDefinition.SampleID, UserID = MockUser.Three.ID, CronString = Testing.AlwaysCronString },
        };
    }
}
