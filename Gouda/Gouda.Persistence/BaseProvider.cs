using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Application.Persistence;
using Gouda.Domain;
using Gouda.Domain.Check;
using Gouda.Domain.Communication;
using System.Linq;
using Curds.Application.Persistence;
using Curds.Persistence.Relationships;

namespace Gouda.Persistence
{
    public abstract class BaseProvider : IProvider
    {
        public Curds.Application.Cron.IProvider Cron { get; set; }

        public IPersistor<Satellite> Satellites { get; }
        public IPersistor<Definition> Definitions { get; }
        public IPersistor<Argument> Arguments { get; }

        public IPersistor<User> Users { get; }
        public IPersistor<Contact> Contacts { get; }

        private CronOneToMany UsersPerDefinition { get; }

        public BaseProvider()
        {
            Satellites = new CachedPersistor<Satellite>(LoadSatellites());
            Definitions = new CachedPersistor<Definition>(LoadDefinitions());
            Arguments = new CachedPersistor<Argument>(LoadArguments());

            Users = new CachedPersistor<User>(LoadUsers());
            Contacts = new CachedPersistor<Contact>(LoadContacts());

            UsersPerDefinition = new CronOneToMany();
        }

        public void LoadRelationships()
        {
            foreach (UserRegistration registration in LoadUserRegistrations())
                UsersPerDefinition.AddRelationship(registration.DefinitionID, registration.UserID, Cron.Build(registration.CronString));
        }

        protected abstract IEnumerable<Satellite> LoadSatellites();
        protected abstract IEnumerable<Definition> LoadDefinitions();
        protected abstract IEnumerable<Argument> LoadArguments();

        protected abstract IEnumerable<User> LoadUsers();
        protected abstract IEnumerable<Contact> LoadContacts();

        protected abstract IEnumerable<UserRegistration> LoadUserRegistrations();

        public IEnumerable<Contact> FilterContacts(int definitionID, DateTime eventTime) => Contacts.Lookup(UsersPerDefinition.Filter(definitionID, eventTime));
        public IEnumerable<Argument> GenerateArguments(int definitionID) => Arguments.Lookup(Definitions.Lookup(definitionID).ArgumentIDs);
    }
}
