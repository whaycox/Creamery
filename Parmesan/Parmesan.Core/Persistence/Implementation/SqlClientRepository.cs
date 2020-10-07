using Curds.Persistence.Implementation;
using Curds.Persistence.Query.Abstraction;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parmesan.Persistence.Implementation
{
    using Abstraction;
    using Domain;

    internal class SqlClientRepository : SqlRepository<IParmesanDataModel, Client>, IClientRepository
    {
        public SqlClientRepository(ISqlQueryBuilder<IParmesanDataModel> queryBuilder)
            : base(queryBuilder)
        { }

        public async Task<Client> FetchByPublicClientID(string publicClientID)
        {
            List<Client> fetched = await FetchEntities(FetchByPublicClientIDQuery(publicClientID));
            if (fetched.Count == 0)
                throw new KeyNotFoundException($"No client found with ClientID: {publicClientID}");
            if (fetched.Count > 1)
                throw new InvalidOperationException($"Duplicate clients found with ClientID: {publicClientID}");
            return fetched[0];
        }
        private ISqlQuery<Client> FetchByPublicClientIDQuery(string publicClientID) => QueryBuilder
            .From<Client>()
            .Where(client => client.PublicClientID == publicClientID)
            .Project();
    }
}
