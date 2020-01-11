using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Persistence.Implementation
{
    using Abstraction;
    using Gouda.Domain;
    using Domain;
    using System.Threading.Tasks;

    internal class EFGoudaDatabase : IGoudaDatabase
    {
        private GoudaContext GoudaContext { get; }

        public IRepository<Satellite> Satellite { get; }
        public IRepository<CheckDefinition> CheckDefinition { get; }
        public IRepository<DiagnosticData> DiagnosticData { get; }

        public EFGoudaDatabase(
            GoudaContext goudaContext,
            IRepository<Satellite> satellites,
            IRepository<CheckDefinition> checks,
            IRepository<DiagnosticData> data)
        {
            GoudaContext = goudaContext;
            GoudaContext.Database.EnsureCreated();

            Satellite = satellites;
            CheckDefinition = checks;
            DiagnosticData = data;
        }

        public Task SaveChanges() => GoudaContext.SaveChangesAsync();
    }
}
