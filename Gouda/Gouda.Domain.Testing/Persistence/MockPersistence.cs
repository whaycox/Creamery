using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Application.Communication;
using Gouda.Application.Persistence;
using Gouda.Domain;
using Gouda.Domain.Communication;
using System.Linq;
using Gouda.Domain.Check;
using Gouda.Persistence;

namespace Gouda.Domain.Persistence
{
    public class MockPersistence : BasePersistence
    {
        private List<Satellite> MockSatellites = new List<Satellite>()
        {
            { MockSatellite.Sample }
        };
        private List<Definition> MockDefinitions = new List<Definition>()
        {
            { MockDefinition.Sample }
        };
        private List<UserRegistration> UserRegistrations = new List<UserRegistration>()
        {
            { new UserRegistration() { UserID = MockUser.One.ID, DefinitionID = MockDefinition.SampleID, CronString = Testing.AlwaysCronString } },
            { new UserRegistration() { UserID = MockUser.Two.ID, DefinitionID = MockDefinition.SampleID, CronString = Testing.AlwaysCronString } },
        };

        protected override IEnumerable<Satellite> LoadSatellites() => MockSatellites;
        protected override IEnumerable<Definition> LoadDefinitions() => MockDefinitions;
        protected override IEnumerable<Argument> LoadArguments() => MockArgument.Samples;

        protected override IEnumerable<User> LoadUsers() => MockUser.Samples;
        protected override IEnumerable<Contact> LoadContacts() => MockContact.Samples;

        protected override IEnumerable<UserRegistration> LoadUserRegistrations() => UserRegistrations;
    }
}
