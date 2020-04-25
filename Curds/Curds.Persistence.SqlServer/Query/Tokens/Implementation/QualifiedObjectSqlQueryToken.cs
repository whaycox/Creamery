﻿using System.Collections.Generic;
using System.Linq;

namespace Curds.Persistence.Query.Tokens.Implementation
{
    using Query.Abstraction;
    using Model.Domain;

    public class QualifiedObjectSqlQueryToken : BaseSqlQueryToken
    {
        private static ConstantSqlQueryToken QualifierToken { get; } = new ConstantSqlQueryToken(".");

        public List<ObjectNameSqlQueryToken> Names { get; }

        public QualifiedObjectSqlQueryToken(Table table)
            : this(
                  new ObjectNameSqlQueryToken(table.Schema),
                  new ObjectNameSqlQueryToken(table.Name))
        { }

        public QualifiedObjectSqlQueryToken(params ObjectNameSqlQueryToken[] names)
        {
            Names = names
                .Where(name => !string.IsNullOrWhiteSpace(name.Name))
                .ToList();
        }

        public override void AcceptFormatVisitor(ISqlQueryFormatVisitor visitor)
        {
            for (int i = 0; i < Names.Count; i++)
            {
                if (i != 0)
                    QualifierToken.AcceptFormatVisitor(visitor);
                Names[i].AcceptFormatVisitor(visitor);
            }
        }
    }
}