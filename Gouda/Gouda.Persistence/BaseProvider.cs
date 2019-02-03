using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Application.Persistence;
using Gouda.Domain;
using Gouda.Domain.Check;
using Gouda.Domain.Communication;
using Gouda.Domain.Persistence;
using System.Linq;

namespace Gouda.Persistence
{
    public abstract class BaseProvider : IProvider
    {
        private EntityCache Cache { get; }

        public BaseProvider()
        {
            Cache = new EntityCache();
        }

        public Definition LookupDefinition(int id) => Cache.Definitions.Retrieve(id);
        public Satellite LookupSatellite(int id) => Cache.Satelites.Retrieve(id);

        public void PopulateCache()
        {
            foreach (Satellite satellite in LoadSatellites())
                Cache.Satelites.AddOrUpdate(satellite);
            foreach (Definition definition in LoadDefinitions())
                Cache.Definitions.AddOrUpdate(definition);
            foreach (Argument argument in LoadArguments())
                Cache.Arguments.AddOrUpdate(argument);

            LoadDefinitionToArguments();
        }
        private void LoadDefinitionToArguments()
        {
            foreach (var group in Cache.Arguments.RetrieveAll().GroupBy(d => d.DefinitionID))
                foreach (var arg in group)
                    Cache.DefinitionToArguments.AddRelationship(group.Key, arg.ID);
        }


        protected abstract IEnumerable<Satellite> LoadSatellites();
        protected abstract IEnumerable<Definition> LoadDefinitions();
        protected abstract IEnumerable<Argument> LoadArguments();
    }
}
