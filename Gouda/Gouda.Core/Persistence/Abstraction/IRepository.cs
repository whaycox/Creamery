using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gouda.Persistence.Abstraction
{
    using Gouda.Domain;

    public interface IRepository<TEntity>
        where TEntity : BaseEntity
    {
        Task Insert(List<TEntity> entities);
        Task<List<TEntity>> FetchMany(List<int> ids);
    }
}
