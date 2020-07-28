using System;
using System.Collections.Generic;
using System.Text;
using Curds.Persistence.Abstraction;
using Curds.Persistence.Implementation;

namespace Cheddar.Persistence.Implementation
{
    using Abstraction;
    using Cheddar.Domain;

    internal class CheddarDatabase : SqlDatabase, ICheddarDatabase
    {
        public ISimpleRepository<ICheddarDataModel, Organization> Organizations { get; }

        public CheddarDatabase(
            ISqlConnectionContext connectionContext,
            ISimpleRepository<ICheddarDataModel, Organization> organizations)
            : base(connectionContext)
        {
            Organizations = organizations;
        }
    }
}
