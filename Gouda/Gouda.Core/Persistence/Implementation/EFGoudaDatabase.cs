using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Persistence.Implementation
{
    using Abstraction;
    using Gouda.Domain;

    public class EFGoudaDatabase : IGoudaDatabase
    {
        public IRepository<Satellite> Satellite => throw new NotImplementedException();
        public IRepository<DiagnosticData> DiagnosticData => throw new NotImplementedException();
    }
}
