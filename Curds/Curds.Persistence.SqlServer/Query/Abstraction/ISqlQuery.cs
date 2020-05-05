using System.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Curds.Persistence.Query.Abstraction
{
    using Persistence.Abstraction;

    public interface ISqlQuery
    {
        void Write(ISqlQueryWriter queryWriter);
        Task ProcessResult(ISqlQueryReader queryReader);
    }

    public interface ISqlQuery<TEntity> : ISqlQuery
        where TEntity : IEntity
    {
        IList<TEntity> Results { get; }
    }
}
