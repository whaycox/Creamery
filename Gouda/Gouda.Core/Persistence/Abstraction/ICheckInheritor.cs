using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gouda.Persistence.Abstraction
{
    using Gouda.Domain;

    public interface ICheckInheritor
    {
        Task<CheckDefinition> Build(int checkID);
        Task<List<CheckDefinition>> Build(List<int> checkIDs);

        Task Seed(List<CheckDefinition> seedChecks);
    }
}
