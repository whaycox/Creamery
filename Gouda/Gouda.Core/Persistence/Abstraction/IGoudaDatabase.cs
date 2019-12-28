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

    public interface IGoudaDatabase
    {
        IRepository<Satellite> Satellite { get; }
        IRepository<CheckDefinition> CheckDefinition { get; }
        IRepository<DiagnosticData> DiagnosticData { get; }

        Task SaveChanges();
    }
}
