using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Query.Formatters.Template
{
    using Implementation;

    public abstract class BaseSqlQueryFormatterTemplate
    {
        protected SimpleIndentStringBuilder TestStringBuilder = new SimpleIndentStringBuilder();
    }
}
