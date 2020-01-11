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

    internal class EFRepository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        private GoudaContext GoudaContext { get; }

        public EFRepository(GoudaContext goudaContext)
        {
            GoudaContext = goudaContext;
        }

        public virtual Task Insert(TEntity entity) => Task.FromResult(GoudaContext
            .Set<TEntity>()
            .Add(entity));

        public Task Insert(List<TEntity> entities) => Task.Run(() => GoudaContext
            .Set<TEntity>()
            .AddRange(entities));

        public async Task<TEntity> Fetch(int id) => await GoudaContext
            .Set<TEntity>()
            .FindAsync(id);

        public Task<List<TEntity>> FetchAll() => GoudaContext
            .Set<TEntity>()
            .ToListAsync();

        public Task<List<TEntity>> FetchMany(List<int> ids) => GoudaContext
            .Set<TEntity>()
            .Where(entity => ids.Contains(entity.ID))
            .ToListAsync();
    }
}
