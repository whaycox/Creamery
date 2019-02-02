using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Application.Persistence;
using Gouda.Domain;
using Gouda.Domain.Check;
using Gouda.Domain.Communication;
using Gouda.Domain.Persistence;

namespace Gouda.Persistence
{
    public abstract class BaseProvider : IProvider
    {
        private EntityCache Cache { get; }

        public BaseProvider()
        {
            Cache = new EntityCache();
        }

        public Satellite LookupSatellite(int id)
        {
            throw new NotImplementedException();
        }

        public void PopulateCache()
        {
            foreach (Satellite satellite in LoadSatellites())
                Cache.Satelites.AddOrUpdate(satellite);
        }

        protected abstract IEnumerable<Satellite> LoadSatellites();
    }
}
