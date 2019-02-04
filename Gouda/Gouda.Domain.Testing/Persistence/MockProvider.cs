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
            { MockSatellite.Sample }
        };
        private List<Definition> MockDefinitions = new List<Definition>()
        {
            { MockDefinition.Sample }
        };

        protected override IEnumerable<Satellite> LoadSatellites() => MockSatellites;
        protected override IEnumerable<Definition> LoadDefinitions() => MockDefinitions;
        protected override IEnumerable<Argument> LoadArguments() => MockArgument.Samples;
        protected override IEnumerable<Contact> LoadContacts() => MockContact.Samples;
    }
}
