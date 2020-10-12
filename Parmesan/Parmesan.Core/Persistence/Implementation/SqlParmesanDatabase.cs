using System;
using System.Collections.Generic;
using System.Text;
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

        public SqlParmesanDatabase(
            ISqlConnectionContext connectionContext,
            IClientRepository clients,
            IUserRepository users,
            IRepository<IParmesanDataModel, PasswordAuthentication> passwords)
            : base(connectionContext)
        {
            Clients = clients;
            Users = users;
            Passwords = passwords;
        }
    }
}
