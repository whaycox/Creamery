using System.Threading.Tasks;
using System;

namespace Curds.Persistence.Abstraction
{
    public interface IDatabase
    {
        Task<IDisposable> BeginTransaction();
        Task CommitTransaction();
    }
}
