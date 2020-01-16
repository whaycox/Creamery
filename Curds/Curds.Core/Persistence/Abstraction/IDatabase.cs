using System.Threading.Tasks;

namespace Curds.Persistence.Abstraction
{
    public interface IDatabase
    {
        Task BeginTransaction();
        Task CommitTransaction();
    }
}
