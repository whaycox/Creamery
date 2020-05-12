using System.Collections.Generic;
using System.Linq;

namespace Curds.Persistence.Query.Tokens.Implementation
{
    using Abstraction;

    public class ColumnListSqlQueryToken : BaseSqlQueryToken
    {
        public List<ISqlColumn> Columns { get; }
        public bool IncludeGrouping { get; set; }
        public bool IncludeDefinition { get; set; }

        public ColumnListSqlQueryToken(IEnumerable<ISqlColumn> columns)
        {
            Columns = columns.ToList();
        }

        public override void AcceptFormatVisitor(ISqlQueryFormatVisitor visitor) => visitor.VisitColumnList(this);
    }
}
