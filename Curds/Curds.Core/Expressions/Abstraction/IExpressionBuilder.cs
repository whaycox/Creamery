﻿using System.Linq.Expressions;
using System.Reflection;
using System.Collections.Generic;
using System;

namespace Curds.Expressions.Abstraction
{
    public interface IExpressionBuilder
    {
        ParameterExpression AddParameter<TEntity>(string name);
        ParameterExpression CreateObject<TEntity>(string name);
        ParameterExpression CreateObject<TEntity>(string name, Type[] constructorTypes, Expression[] constructorValues);

        void SetProperty(ParameterExpression variable, PropertyInfo property, Expression value);

        void For(Expression collectionExpression, Func<ParameterExpression, Expression> contentExpressionDelegate);

        void ReturnObject(ParameterExpression variable);

        TDelegate Build<TDelegate>();
    }
}
