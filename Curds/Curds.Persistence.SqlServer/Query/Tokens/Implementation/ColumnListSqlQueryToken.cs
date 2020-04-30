using System.Collections.Generic;
using System.Linq;

namespace Curds.Persistence.Query.Tokens.Implementation
{
    using Abstraction;
    using Model.Abstraction;

    public class ColumnListSqlQueryToken : BaseSqlQueryToken
    {
        public List<IValueModel> Values { get; }
        public bool IncludeGrouping { get; set; }
        public bool IncludeDefinition { get; set; }

        public ColumnListSqlQueryToken(IEnumerable<IValueModel> values)
        {
            Values = values.ToList();
        }

        public override void AcceptFormatVisitor(ISqlQueryFormatVisitor visitor) => visitor.VisitColumnList(this);
    }
}
