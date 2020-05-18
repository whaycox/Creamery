namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;
    using Model.Abstraction;
    using System.Data;

    public class SqlColumn : ISqlColumn
    {
        public ISqlTable Table { get; }
        public IValueModel ValueModel { get; }

        public string ValueName => ValueModel.Property.Name;
        public string Name => ValueModel.Name;
        public SqlDbType Type => ValueModel.SqlType;

        public SqlColumn(ISqlTable table, IValueModel valueModel)
        {
            Table = table;
            ValueModel = valueModel;
        }
    }
}
