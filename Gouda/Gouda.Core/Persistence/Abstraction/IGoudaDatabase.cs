using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Persistence.Abstraction
{
    using Gouda.Domain;

    public interface IGoudaDatabase
    {
        IRepository<Satellite> Satellite { get; }
        IRepository<DiagnosticData> DiagnosticData { get; }
    }
}
