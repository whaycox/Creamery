using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Curds.Persistence.Persistor.Abstraction
{
    using Security.Domain;

    public interface IUserPersistor<T> : IEntityPersistor<T> where T : User
    {
        Task<User> FindByEmail(string email);
    }
}
