using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;
    using Persistence.Abstraction;
    using Model.Abstraction;

    internal class SqlQueryContext<TModel> : ISqlQueryContext<TModel>
        where TModel : IDataModel
    {
        private IModelMap<TModel> ModelMap { get; }
        private IExpressionNodeFactory ExpressionNodeFactory { get; }
        private ISqlQueryTokenVisitor<TModel> TokenVisitor { get; }
        private ISqlTableVisitor<TModel> TableVisitor { get; }

        public ISqlQueryTokenFactory TokenFactory { get; }
        public ISqlQueryFormatter Formatter { get; }
        public ISqlQueryParameterBuilder ParameterBuilder { get; }

        private List<ISqlTable> QueryTables { get; } = new List<ISqlTable>();
        public IList<ISqlTable> Tables => QueryTables.ToList();

        public SqlQueryContext(
            IModelMap<TModel> modelMap,
            IExpressionNodeFactory experssionNodeFactory,
            ISqlQueryTokenVisitor<TModel> tokenVisitor,
            ISqlTableVisitor<TModel> tableVisitor,
            ISqlQueryTokenFactory tokenFactory,
            ISqlQueryFormatter formatter,
            ISqlQueryParameterBuilder parameterBuilder)
        {
            ModelMap = modelMap;
            ExpressionNodeFactory = experssionNodeFactory;
            TokenVisitor = tokenVisitor;
            TableVisitor = tableVisitor;

            TokenFactory = tokenFactory;
            Formatter = formatter;
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
            IExpressionNode<ISqlQueryToken, ISqlQueryContext<TModel>> rootNode = ExpressionNodeFactory.Build<ISqlQueryToken, ISqlQueryContext<TModel>>(queryExpression);
            return rootNode.AcceptVisitor(TokenVisitor, this);
        }

        public ISqlTable ParseTableExpression(Expression tableExpression)
        {
            IExpressionNode<ISqlTable, ISqlQueryContext<TModel>> rootNode = ExpressionNodeFactory.Build<ISqlTable, ISqlQueryContext<TModel>>(tableExpression);
            return rootNode.AcceptVisitor(TableVisitor, this);
        }
    }
}
