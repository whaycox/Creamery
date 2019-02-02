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
        private const int MockDefinitionID = 5;

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

        protected override IEnumerable<Satellite> LoadSatellites() => MockSatellites;
    }
}
