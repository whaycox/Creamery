using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;
    using Model.Abstraction;
    using Persistence.Abstraction;

    internal class SqlQueryContext<TModel> : ISqlQueryContext<TModel>
        where TModel : IDataModel
    {
        private IModelMap<TModel> ModelMap { get; }
        private IExpressionNodeFactory ExpressionNodeFactory { get; }
        private ISqlQueryExpressionVisitorFactory ExpressionVisitorFactory { get; }

        public ISqlQueryFormatter Formatter { get; }
        public ISqlConnectionContext ConnectionContext { get; }
        public ISqlQueryParameterBuilder ParameterBuilder { get; }

        private List<ISqlTable> QueryTables { get; } = new List<ISqlTable>();
        public IList<ISqlTable> Tables => QueryTables.ToList();

        public SqlQueryContext(
            IModelMap<TModel> modelMap,
            IExpressionNodeFactory experssionNodeFactory,
            ISqlQueryExpressionVisitorFactory expressionVisitorFactory,
            ISqlQueryFormatter formatter,
            ISqlConnectionContext connectionContext,
            ISqlQueryParameterBuilder parameterBuilder)
        {
            ModelMap = modelMap;
            ExpressionNodeFactory = experssionNodeFactory;
            ExpressionVisitorFactory = expressionVisitorFactory;

            Formatter = formatter;
            ConnectionContext = connectionContext;
            ParameterBuilder = parameterBuilder;
        }

        public ISqlTable AddTable<TEntity>()
            where TEntity : IEntity
        {
            SqlTable table = new SqlTable { Model = ModelMap.Entity<TEntity>() };
            QueryTables.Add(table);
            return table;
        }

        public ISqlQueryToken ParseQueryExpression(Expression queryExpression)
        {
            IExpressionNode rootNode = ExpressionNodeFactory.Build(queryExpression);
            return rootNode.AcceptVisitor(ExpressionVisitorFactory.TokenVisitor(this));
        }

        public ISqlTable ParseTableExpression(Expression tableExpression)
        {
            IExpressionNode rootNode = ExpressionNodeFactory.Build(tableExpression);
            return rootNode.AcceptVisitor(ExpressionVisitorFactory.TableVisitor(this));
        }
    }
}
