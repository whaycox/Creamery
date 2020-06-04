using System.Collections.Generic;
using System.Threading.Tasks;

namespace Curds.Persistence.Implementation
{
    using Abstraction;
    using Domain;
    using Query.Abstraction;

    public class CustomTestEntityRepository : SqlRepository<ITestDataModel, TestEntity>
    {
        public CustomTestEntityRepository(
            ISqlConnectionContext connectionContext,
            ISqlQueryBuilder<ITestDataModel> queryBuilder)
            : base(connectionContext, queryBuilder)
        { }

        public Task<IList<TestEntity>> FetchEvensLessThan(int maxID) => ConnectionContext.ExecuteWithResult(
            QueryBuilder.From<TestEntity>()
            .Where(entity => entity.ID <= maxID)
            .Where(entity => entity.ID % 2 == 0)
            .ProjectEntity());
    }
}
