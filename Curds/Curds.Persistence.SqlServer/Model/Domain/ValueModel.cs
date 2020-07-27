using System.Data;
using System.Reflection;

namespace Curds.Persistence.Model.Domain
{
    using Abstraction;

    public class ValueModel : IValueModel
    {
        public string Name { get; set; }

        public PropertyInfo Property { get; set; }
        public SqlDbType SqlType { get; set; }

        public bool IsIdentity { get; set; }

    }
}
