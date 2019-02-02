using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Curds.Domain.Persistence;
using Curds.Domain.Persistence.Relationships;

namespace Gouda.Domain.Persistence
{
    using Communication;
    using Check;

    public class EntityCache
    {
        public Cache<Satellite> Satelites = new Cache<Satellite>();
        public CachedRelationship DefinitionToSatellite = new OneToOne();
        public Cache<Definition> Definitions = new Cache<Definition>();
        public CachedRelationship DefinitionToArguments = new OneToMany();
        public Cache<Argument> Arguments = new Cache<Argument>();

        public Cache<User> Users = new Cache<User>();
    }
}
