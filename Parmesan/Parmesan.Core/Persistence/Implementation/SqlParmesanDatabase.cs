using Curds.Persistence.Abstraction;
using Curds.Persistence.Implementation;

namespace Parmesan.Persistence.Implementation
{
    using Abstraction;
    using Parmesan.Domain;

    internal class SqlParmesanDatabase : SqlDatabase, IParmesanDatabase
    {
        public IClientRepository Clients { get; }
        public IUserRepository Users { get; }
        public IRepository<IParmesanDataModel, PasswordAuthentication> Passwords { get; }
        public IRepository<IParmesanDataModel, AuthorizationGrant> AuthorizationGrants { get; }
        public IRepository<IParmesanDataModel, AuthorizationCode> AuthorizationCodes { get; }

        public SqlParmesanDatabase(
            ISqlConnectionContext connectionContext,
            IClientRepository clients,
            IUserRepository users,
            IRepository<IParmesanDataModel, PasswordAuthentication> passwords,
            IRepository<IParmesanDataModel, AuthorizationGrant> authorizationGrants,
            IRepository<IParmesanDataModel, AuthorizationCode> authorizationCodes)
            : base(connectionContext)
        {
            Clients = clients;
            Users = users;
            Passwords = passwords;
            AuthorizationGrants = authorizationGrants;
            AuthorizationCodes = authorizationCodes;
        }
    }
}
