using System.Data;

namespace Curds.Persistence.Query.Abstraction
{
    public interface ISqlColumn
    {
        ISqlTable Table { get; }

        string ValueName { get; }
        string Name { get; }
        SqlDbType Type { get; }
    }
}
