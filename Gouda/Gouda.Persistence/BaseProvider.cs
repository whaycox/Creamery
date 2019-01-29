using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Application.Persistence;
using Gouda.Domain;
using Gouda.Domain.Check;
using Gouda.Domain.Communication;

namespace Gouda.Persistence
{
    public abstract class BaseProvider : IProvider
    {
        public EntityCollection Load()
        {
            EntityCollection loaded = new EntityCollection();

            loaded.Add(LoadSatellites());
            loaded.Add(LoadDefinitions(loaded.SatelliteMap));
            loaded.Add(LoadUsers());

            return loaded;
        }
        protected abstract IEnumerable<Satellite> LoadSatellites();
        protected abstract IEnumerable<Definition> LoadDefinitions(Dictionary<int, Satellite> satellites);
        protected abstract IEnumerable<User> LoadUsers();

        public abstract IEnumerable<UserRegistration> LoadRegistrations(IEnumerable<Definition> definitions);
        public abstract IEnumerable<Contact> LoadContacts(IEnumerable<User> users);
    }
}
