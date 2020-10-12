using System;
using System.Collections.Generic;
using System.Text;
using Curds.Persistence.Implementation;
using Curds.Persistence.Query.Abstraction;
using System.Threading.Tasks;

namespace Parmesan.Persistence.Implementation
{
    using Parmesan.Domain;
    using Abstraction;

    internal class SqlUserRepository : SqlRepository<IParmesanDataModel, User>, IUserRepository
    {
        public SqlUserRepository(ISqlQueryBuilder<IParmesanDataModel> queryBuilder)
            : base(queryBuilder)
        { }

        public Task<User> FetchByUserName(string userName) => FetchSingleEntity(FetchByUserNameQuery(userName));
        private ISqlQuery<User> FetchByUserNameQuery(string userName) => QueryBuilder
            .From<User>()
            .Where(user => user.UserName == userName)
            .Project();
    }
}
