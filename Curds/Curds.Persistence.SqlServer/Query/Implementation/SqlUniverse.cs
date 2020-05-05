using System.Collections.Generic;
using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq;

namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;
    using Model.Abstraction;
    using Persistence.Abstraction;
    using Persistence.Domain;
    using Domain;

    internal abstract class SqlUniverse : ISqlUniverse
    {
        private IModelMap ModelMap { get; }

        public IEnumerable<ISqlTable> Tables => TableCollection;
        private List<ISqlTable> TableCollection { get; } = new List<ISqlTable>();

        public IEnumerable<ISqlUniverseFilter> Filters => FilterCollection;
        private List<ISqlUniverseFilter> FilterCollection { get; } = new List<ISqlUniverseFilter>();

        public SqlUniverse(IModelMap modelMap)
        {
            ModelMap = modelMap;
        }

        protected ISqlTable AddTable<TEntity>()
            where TEntity : IEntity
        {
            SqlTable table = new SqlTable { Model = ModelMap.Entity<TEntity>() };
            TableCollection.Add(table);
            return table;
        }

        protected void AddFilter(ISqlUniverseFilter universeFilter) => FilterCollection.Add(universeFilter);
    }

    internal class SqlUniverse<TEntity> : SqlUniverse, ISqlUniverse<TEntity>
        where TEntity : IEntity
    {
        private ISqlTable Table { get; }

        public SqlUniverse(IModelMap modelMap)
            : base(modelMap)
        {
            Table = AddTable<TEntity>();
        }

        public ISqlQuery<TEntity> ProjectEntity() => new ProjectEntityQuery<TEntity> { ProjectedTable = Table, Source = this };

        public ISqlUniverse<TEntity> Where(Expression<Func<TEntity, bool>> filterExpression)
        {
            object[] suppliedKeys = ParseKeyEqualityExpression(filterExpression);
            IList<ISqlColumn> keys = Table.Keys;
            if (suppliedKeys.Length != keys.Count)
                throw new Exception();

            for (int i = 0; i < suppliedKeys.Length; i++)
                AddFilter(new ConstantValueSqlUniverseFilter(SqlBooleanOperation.Equals, keys[i], suppliedKeys[i]));

            return this;
        }
        private object[] ParseKeyEqualityExpression(Expression<Func<TEntity, bool>> filterExpression)
        {
            if (filterExpression.NodeType != ExpressionType.Lambda)
                throw new InvalidExpressionException(filterExpression);

            Expression lambdaBody = filterExpression.Body;
            switch (lambdaBody.NodeType)
            {
                case ExpressionType.Equal:
                    BinaryExpression binaryExpression = (BinaryExpression)lambdaBody;
                    Expression left = binaryExpression.Left;
                    MemberExpression leftMember = (MemberExpression)left;
                    if (leftMember.Member.Name != nameof(IEntity.Keys))
                        throw new InvalidExpressionException(filterExpression);
                    UnaryExpression leftUnary = (UnaryExpression)leftMember.Expression;
                    if (leftUnary.NodeType != ExpressionType.Convert ||
                        !(leftUnary.Operand is ParameterExpression))
                        throw new InvalidExpressionException(filterExpression);

                    Expression right = binaryExpression.Right;
                    MemberExpression rightMember = (MemberExpression)right;
                    if (rightMember.Type != typeof(object[]))
                        throw new InvalidExpressionException(filterExpression);
                    ConstantExpression rightConstant = (ConstantExpression)rightMember.Expression;
                    FieldInfo rightField = (FieldInfo)rightMember.Member;
                    return (object[])rightField.GetValue(rightConstant.Value);
                default:
                    throw new InvalidExpressionException(filterExpression);
            }
        }
    }
}
