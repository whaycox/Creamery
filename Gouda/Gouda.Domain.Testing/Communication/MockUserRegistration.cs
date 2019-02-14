using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain.Security;

namespace Gouda.Domain.Communication
{
    using Check;

    public class MockUserRegistration : UserRegistration
    {
        public const int SampleID = 13;

        public static MockUserRegistration Sample =>
            new MockUserRegistration
            {
                ID = SampleID,
                DefinitionID = MockDefinition.SampleID,
                UserID = MockUser.One.ID,
                CronString = Testing.AlwaysCronString,
            };
    }
}
