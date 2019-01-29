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
    public class MockProvider : BaseProvider
    {
        private List<Satellite> MockSatellites = new List<Satellite>()
        {
            { new MockSatellite() }
        };
        private List<User> MockUsers = new List<User>()
        {
            { MockUser.One },
            { MockUser.Two },
            { MockUser.Three },
        };

        protected override IEnumerable<Definition> LoadDefinitions(Dictionary<int, Satellite> satellites)
        {
            List<Definition> toReturn = new List<Definition>();
            toReturn.Add(new MockDefinition(satellites[0]));
            return toReturn;
        }

        protected override IEnumerable<Satellite> LoadSatellites() => MockSatellites;

        protected override IEnumerable<User> LoadUsers() => MockUsers;

        public override IEnumerable<UserRegistration> LoadRegistrations(IEnumerable<Definition> definitions) => new List<UserRegistration>
        {
            { new UserRegistration() { DefinitionID = MockDefinition.MockID, UserID = 2, CronString = Testing.AlwaysCronString } },
            { new UserRegistration() { DefinitionID = MockDefinition.MockID, UserID = 3, CronString = Testing.AlwaysCronString } }
        };

        public override IEnumerable<Contact> LoadContacts(IEnumerable<User> users) => new List<Contact>()
        {
            { new MockContactOne() { UserID = 3, CronString = Testing.AlwaysCronString} },
            { new MockContactTwo() { UserID = 2, CronString = Testing.AlwaysCronString} },
        };
    }
}
