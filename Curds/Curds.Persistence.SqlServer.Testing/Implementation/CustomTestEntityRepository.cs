using System.Collections.Generic;
using System.Threading.Tasks;

namespace Curds.Persistence.Implementation
{
    using Abstraction;
    using Domain;
    using Query.Abstraction;

    public class CustomTestEntityRepository : SqlRepository<ITestDataModel, TestEntity>
    {
        public CustomTestEntityRepository(ISqlQueryBuilder<ITestDataModel> queryBuilder)
            : base(queryBuilder)
        { }

        public Task<IList<TestEntity>> FetchEvensLessThan(int maxID) => FetchEntities(
            QueryBuilder.From<TestEntity>()
            .Where(entity => entity.ID <= maxID)
            .Where(entity => entity.ID % 2 == 0)
            .Project());
    }
}
