using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Linq;
using System;

namespace Curds.Persistence.Query.Tokens.Implementation
{
    using Query.Domain;

    public class KeywordSqlQueryToken : LiteralSqlQueryToken
    {
        public override string Literal => ComputeLiteral(Keyword);
        public SqlQueryKeyword Keyword { get; }

        public KeywordSqlQueryToken(SqlQueryKeyword keyword)
        {
            Keyword = keyword;
        }

        private string ComputeLiteral(SqlQueryKeyword keyword)
        {
            string baseKeyword = keyword.ToString();
            MemberInfo enumeration = typeof(SqlQueryKeyword)
                .GetMember(baseKeyword, BindingFlags.Public | BindingFlags.Static)
                .First();
            DisplayAttribute displayAttribute = enumeration.GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;
            return displayAttribute != null ? 
                displayAttribute.Name : 
                baseKeyword;
        }
    }
}
