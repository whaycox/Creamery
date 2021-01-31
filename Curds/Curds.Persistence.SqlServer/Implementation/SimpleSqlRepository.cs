using System.Threading.Tasks;

namespace Curds.Persistence.Implementation
{
    using Abstraction;
    using Query.Abstraction;

    public class SimpleSqlRepository<TDataModel, TEntity> : SqlRepository<TDataModel, TEntity>, ISimpleRepository<TDataModel, TEntity>
        where TDataModel : IDataModel
        where TEntity : class, ISimpleEntity
    {
        public SimpleSqlRepository(ISqlQueryBuilder<TDataModel> queryBuilder)
            : base(queryBuilder)
        { }

        public Task<TEntity> Fetch(int id) => Fetch(keys: id);
        public IEntityUpdate<TEntity> Update(int id) => Update(keys: id);
        public Task Delete(int id) => Delete(keys: id);
    }
}
