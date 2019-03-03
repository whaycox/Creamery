using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application.Security;
using Gouda.Domain.Security;
using System.Threading.Tasks;
using Curds.Application.DateTimes;

namespace Gouda.Application.Security
{
    using Persistence;

    public interface ISecurity : IAuthenticator
    {
        IDateTime Time { get; }
        IPersistence Persistence { get; }

        string GenerateSessionIdentifier();
        Task<Session> GenerateSession(string deviceIdentifier, string email, string password);
    }
}
