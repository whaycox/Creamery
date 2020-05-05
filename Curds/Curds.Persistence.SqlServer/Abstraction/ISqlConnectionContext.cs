using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Curds.Persistence.Abstraction
{
    using Query.Abstraction;

    public interface ISqlConnectionContext : IDisposable
    {
        Task BeginTransaction();
        Task RollbackTransaction();
        Task CommitTransaction();

        Task Execute(ISqlQuery query);
        Task ExecuteWithResult(ISqlQuery query);
        Task<IList<TEntity>> ExecuteWithResult<TEntity>(ISqlQuery<TEntity> query)
            where TEntity : IEntity;
    }
}
