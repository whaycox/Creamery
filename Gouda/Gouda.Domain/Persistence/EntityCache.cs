using Curds.Domain.Persistence;
using Curds.Domain.Persistence.Relationships;

namespace Gouda.Domain.Persistence
{
    using Check;
    using Communication;

    public class EntityCache
    {
        public Cache<Satellite> Satelites = new Cache<Satellite>();
        public Cache<Definition> Definitions = new Cache<Definition>();
        public CachedRelationship DefinitionToArguments = new OneToMany();
        public Cache<Argument> Arguments = new Cache<Argument>();

        public Cache<User> Users = new Cache<User>();
    }
}
