using System;
using System.Threading.Tasks;

namespace Curds.Persistence.Abstraction
{
    public interface IDatabaseTransaction : IDisposable
    {
        Task RollbackTransaction();
        Task CommitTransaction();
    }
}
