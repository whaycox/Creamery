using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gouda.Persistence.Repositories.Implementation
{
    using Gouda.Domain;
    using Persistence.Domain;
    using Persistence.Implementation;
    using Persistence.Abstraction;

    internal class CheckDefinitionRepository : EFRepository<CheckDefinition>
    {
        private ICheckInheritor CheckInheritor { get; }

        public CheckDefinitionRepository(
            GoudaContext goudaContext, 
            ICheckInheritor checkInheritor)
            : base(goudaContext)
        {
            CheckInheritor = checkInheritor;
        }

        public async override Task Insert(CheckDefinition entity)
        {
            await base.Insert(entity);
            await CheckInheritor.Add(entity);
        }
    }
}
