using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Abstraction
{
    using Security.Domain;
    using Persistor.Abstraction;

    public interface ISecurityPersistence
    {
        IUserPersistor<User> Users { get; }
        ISessionPersistor<Session> Sessions { get; }
        IReAuthPersistor<ReAuth> ReAuthentications { get; }
    }
}
