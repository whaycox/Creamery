using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;
    using Model.Abstraction;
    using System.Data;

    public class SqlColumn : ISqlColumn
    {
        private IValueModel ValueModel { get; }

        public ISqlTable Table { get; }

        public string Name => ValueModel.Name;
        public SqlDbType Type => ValueModel.SqlType;

        public SqlColumn(ISqlTable table, IValueModel valueModel)
        {
            Table = table;
            ValueModel = valueModel;
        }
    }
}
