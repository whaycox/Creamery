using System.Collections.Generic;
using System.Threading.Tasks;

namespace Curds.Persistence.Abstraction
{
    using Domain;

    public interface IChildRepository : ISimpleRepository<ITestDataModel, Child>
    {
        Task<List<Child>> ChildrenByParent(int parentID);
    }
}
