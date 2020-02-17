using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Curds.Persistence.Query.Abstraction
{
    using Persistence.Domain;

    public interface ISqlQuery
    {
        void Write(ISqlQueryWriter queryWriter);
        Task ProcessResult(ISqlQueryReader queryReader);
    }
}
