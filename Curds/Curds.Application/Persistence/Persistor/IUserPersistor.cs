using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain.Security;
using System.Threading.Tasks;

namespace Curds.Application.Persistence.Persistor
{
    public interface IUserPersistor<T> : IEntityPersistor<T> where T : User
    {
        Task<User> FindByEmail(string email);
    }
}
