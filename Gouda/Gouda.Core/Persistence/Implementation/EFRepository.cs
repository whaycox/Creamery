using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gouda.Persistence.Implementation
{
    using Abstraction;
    using Gouda.Domain;

    public class EFRepository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        public Task Insert(List<TEntity> entities)
        {
            throw new NotImplementedException();
        }
    }
}
