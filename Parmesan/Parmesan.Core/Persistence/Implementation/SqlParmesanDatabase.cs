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

        public SqlParmesanDatabase(
            ISqlConnectionContext connectionContext,
            IClientRepository clients)
            : base(connectionContext)
        {
            Clients = clients;
        }
    }
}
