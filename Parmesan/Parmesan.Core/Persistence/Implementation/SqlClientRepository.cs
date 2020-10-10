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
            Client fetched = await FetchSingleEntity(FetchByPublicClientIDQuery(publicClientID));
            List<ClientRedirectionUri> redirects = await FetchEntities(FetchRedirectsForClientQuery(fetched.ID));
            fetched.Redirects.AddRange(redirects);
            return fetched;
        }
        private ISqlQuery<Client> FetchByPublicClientIDQuery(string publicClientID) => QueryBuilder
            .From<Client>()
            .Where(client => client.PublicClientID == publicClientID)
            .Project();
        private ISqlQuery<ClientRedirectionUri> FetchRedirectsForClientQuery(int clientID) => QueryBuilder
            .From<ClientRedirectionUri>()
            .Where(redirect => redirect.ClientID == clientID)
            .Project();
    }
}
