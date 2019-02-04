using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Application.Persistence;
using Gouda.Domain;
using Gouda.Domain.Check;
using Gouda.Domain.Communication;
using System.Linq;
using Curds.Application.Persistence;

namespace Gouda.Persistence
{
    public abstract class BaseProvider : IProvider
    {
        public Curds.Application.Cron.IProvider Cron { get; set; }

        public IPersistor<Satellite> Satellites { get; }
        public IPersistor<Definition> Definitions { get; }
        public IPersistor<Argument> Arguments { get; }
        public IPersistor<Contact> Contacts { get; }

        public BaseProvider()
        {
            Satellites = new CachedPersistor<Satellite>(LoadSatellites());
            Definitions = new CachedPersistor<Definition>(LoadDefinitions());
            Arguments = new CachedPersistor<Argument>(LoadArguments());
            Contacts = new CachedPersistor<Contact>(LoadContacts());
        }

        protected abstract IEnumerable<Satellite> LoadSatellites();
        protected abstract IEnumerable<Definition> LoadDefinitions();
        protected abstract IEnumerable<Argument> LoadArguments();
        protected abstract IEnumerable<Contact> LoadContacts();

        public IEnumerable<Contact> LookupContacts(int definitionID, DateTime eventTime)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Contact> FilterContacts(int definitionID, DateTime eventTime)
        {
            throw new NotImplementedException();
        }
    }
}
