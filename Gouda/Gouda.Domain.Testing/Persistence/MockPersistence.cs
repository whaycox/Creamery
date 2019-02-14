using Gouda.Domain.Check;
using Gouda.Domain.Communication;
using Gouda.Persistence;
using System.Collections.Generic;
using System.Linq;
using Gouda.Domain.Security;

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

        public void EmptyUsers()
        {
            foreach (int userID in Users.FetchAll().Select(u => u.ID))
                Users.Delete(userID);
        }

    }
}
