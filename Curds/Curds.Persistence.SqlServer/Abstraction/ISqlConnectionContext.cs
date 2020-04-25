using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Curds.Persistence.Abstraction
{
    using Domain;
    using Query.Abstraction;

    public interface ISqlConnectionContext : IDisposable
    {
        Task BeginTransaction();
        Task RollbackTransaction();
        Task CommitTransaction();

        Task Execute(ISqlQuery query);
        Task ExecuteWithResult(ISqlQuery query);
        Task<List<TEntity>> ExecuteWithResult<TEntity>(ISqlQuery<TEntity> query)
            where TEntity : BaseEntity;
    }
}
