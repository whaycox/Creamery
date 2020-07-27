using System.Threading.Tasks;

namespace Curds.Persistence.Abstraction
{
    public interface IDatabase
    {
        Task<IDatabaseTransaction> BeginTransaction();
    }
}
