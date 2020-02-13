using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        Task<List<TEntity>> Execute<TEntity>(ISqlQuery<TEntity> query)
            where TEntity : BaseEntity;
    }
}
