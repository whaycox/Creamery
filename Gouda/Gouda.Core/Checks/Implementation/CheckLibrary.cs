using System;
using System.Collections.Generic;
using System.Linq;

namespace Gouda.Checks.Implementation
{
    using Abstraction;
    using Gouda.Abstraction;

    public class CheckLibrary : ICheckLibrary
    {
        private Dictionary<Guid, ICheck> Checks = new Dictionary<Guid, ICheck>();

        public List<ICheck> RegisteredChecks => Checks.Values.ToList();

        public CheckLibrary()
        {
            AddCheckToLibrary(new HeartbeatCheck());
        }
        private void AddCheckToLibrary(ICheck check) => Checks.Add(check.ID, check);
    }
}
