using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Curds.Persistence.Abstraction
{
    using Domain;

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
