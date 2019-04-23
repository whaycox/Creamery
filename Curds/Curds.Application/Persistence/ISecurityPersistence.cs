using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain.Security;

namespace Curds.Application.Persistence
{
    using Persistor;

    public interface ISecurityPersistence
    {
        IUserPersistor<User> Users { get; }
        ISessionPersistor<Session> Sessions { get; }
        IReAuthPersistor<ReAuth> ReAuthentications { get; }
    }
}
