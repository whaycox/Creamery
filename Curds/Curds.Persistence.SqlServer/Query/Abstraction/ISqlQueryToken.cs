using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Query.Abstraction
{
    using Text.Abstraction;

    public interface ISqlQueryToken
    {
        void AcceptFormatVisitor(ISqlQueryFormatVisitor visitor);
    }
}
