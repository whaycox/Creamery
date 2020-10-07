using System.Collections.Generic;
using System.Threading.Tasks;

namespace Curds.Persistence.Implementation
{
    using Abstraction;
    using Domain;
    using Query.Abstraction;

    public abstract class BaseSqlRepository
    {
        protected async Task<TEntity> FetchSingleEntity<TEntity>(ISqlQuery<TEntity> entityQuery)
            where TEntity : IEntity
        {
            List<TEntity> entities = await FetchEntities(entityQuery);
            if (entities.Count == 0)
                throw new QueryResultCountException("No entities found with the supplied keys");
            if (entities.Count != 1)
                throw new QueryResultCountException($"Expected 1 entity but found {entities.Count} instead");
            return entities[0];
        }
        protected async Task<List<TEntity>> FetchEntities<TEntity>(ISqlQuery<TEntity> entityQuery)
            where TEntity : IEntity
        {
            await entityQuery.Execute();
            return entityQuery.Results;
        }
    }
}
