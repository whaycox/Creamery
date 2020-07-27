using System.Data;
using System.Reflection;

namespace Curds.Persistence.Model.Abstraction
{
    public interface IValueModel
    {
        string Name { get; }

        PropertyInfo Property { get; }
        SqlDbType SqlType { get; }

        bool IsIdentity { get; }
    }
}
