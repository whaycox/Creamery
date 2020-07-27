using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Curds.Persistence.Query.Abstraction
{
    public interface ISqlQueryReaderFactory
    {
        Task<ISqlQueryReader> Create(SqlCommand command);
    }
}
