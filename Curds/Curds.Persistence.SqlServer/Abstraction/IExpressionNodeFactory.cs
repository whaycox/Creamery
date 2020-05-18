using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace Curds.Persistence.Abstraction
{
    internal interface IExpressionNodeFactory
    {
        IExpressionNode<TReturn, TContext> Build<TReturn, TContext>(Expression expression);
    }
}
