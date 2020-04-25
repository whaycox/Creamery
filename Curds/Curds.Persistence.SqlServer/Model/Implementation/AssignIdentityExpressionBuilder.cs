using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;

namespace Curds.Persistence.Model.Implementation
{
    using Abstraction;
    using Persistence.Abstraction;
    using Query.Abstraction;

    internal class AssignIdentityExpressionBuilder : BaseExpressionBuilder, IAssignIdentityExpressionBuilder
    {
        private Dictionary<Type, Func<ParameterExpression, MethodInfo, ParameterExpression, Expression>> ReadIdentityMap { get; }

        public AssignIdentityExpressionBuilder()
        {
            ReadIdentityMap = new Dictionary<Type, Func<ParameterExpression, MethodInfo, ParameterExpression, Expression>>
            {
                { typeof(byte), ReadByteIdentity },
                { typeof(short), ReadShortIdentity },
                { typeof(int), ReadIntIdentity },
                { typeof(long), ReadLongIdentity },
            };
        }

        private Expression ReadIdentity(ParameterExpression queryReaderParameter, string methodName)
        {
            MethodInfo getIdentityMethod = typeof(ISqlQueryReader).GetMethod(methodName);
            return Expression.Call(queryReaderParameter, getIdentityMethod, Expression.Constant(0, typeof(int)));
        }
        private Expression ReadByteIdentity(ParameterExpression entityParameter, MethodInfo setIdentityMethod, ParameterExpression queryReaderParameter) =>
            CallMethodExpression<byte>(entityParameter, setIdentityMethod, ReadIdentity(queryReaderParameter, nameof(ISqlQueryReader.ReadByte)));
        private Expression ReadShortIdentity(ParameterExpression entityParameter, MethodInfo setIdentityMethod, ParameterExpression queryReaderParameter) =>
            CallMethodExpression<short>(entityParameter, setIdentityMethod, ReadIdentity(queryReaderParameter, nameof(ISqlQueryReader.ReadShort)));
        private Expression ReadIntIdentity(ParameterExpression entityParameter, MethodInfo setIdentityMethod, ParameterExpression queryReaderParameter) =>
            CallMethodExpression<int>(entityParameter, setIdentityMethod, ReadIdentity(queryReaderParameter, nameof(ISqlQueryReader.ReadInt)));
        private Expression ReadLongIdentity(ParameterExpression entityParameter, MethodInfo setIdentityMethod, ParameterExpression queryReaderParameter) =>
            CallMethodExpression<long>(entityParameter, setIdentityMethod, ReadIdentity(queryReaderParameter, nameof(ISqlQueryReader.ReadLong)));

        public AssignIdentityDelegate BuildAssignIdentityDelegate(Type entityType, PropertyInfo identityProperty)
        {
            ParameterExpression queryReaderParameter = Expression.Parameter(typeof(ISqlQueryReader), nameof(queryReaderParameter));
            ParameterExpression iEntityParameter = Expression.Parameter(typeof(IEntity), nameof(iEntityParameter));
            ParameterExpression entityParameter = Expression.Parameter(entityType, nameof(entityParameter));
            List<ParameterExpression> builderExpressionParameters = new List<ParameterExpression>
            {
                entityParameter,
            };

            MethodInfo setIdentityMethod = identityProperty.SetMethod;
            var readIdentityDelegate = ReadIdentityMap[identityProperty.PropertyType];
            List<Expression> builderExpressions = new List<Expression>
            {
                Expression.Assign(entityParameter, Expression.Convert(iEntityParameter, entityType)),
                readIdentityDelegate(entityParameter, setIdentityMethod, queryReaderParameter),
            };

            BlockExpression builderBlock = Expression.Block(builderExpressionParameters, builderExpressions);

            return Expression
                .Lambda<AssignIdentityDelegate>(builderBlock, new ParameterExpression[] { queryReaderParameter, iEntityParameter })
                .Compile();
        }
    }
}
