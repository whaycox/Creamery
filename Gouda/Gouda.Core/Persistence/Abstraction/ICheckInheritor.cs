using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gouda.Persistence.Abstraction
{
    using Gouda.Domain;

    public interface ICheckInheritor
    {
        Task<Check> Build(int checkID);
        Task<List<Check>> Build(List<int> checkIDs);

        Task Seed(List<Check> seedChecks);
    }
}
