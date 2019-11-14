using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Gouda.Persistence.Implementation
{
    using Abstraction;
    using Gouda.Domain;
    using Domain;

    public class EFRepository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        private GoudaContext GoudaContext { get; }

        public EFRepository(GoudaContext goudaContext)
        {
            GoudaContext = goudaContext;
        }

        public Task<List<TEntity>> FetchMany(List<int> ids) => GoudaContext
            .Set<TEntity>()
            .Where(entity => ids.Contains(entity.ID))
            .ToListAsync();

        public Task Insert(List<TEntity> entities) => GoudaContext
            .Set<TEntity>()
            .AddRangeAsync(entities);
    }
}
