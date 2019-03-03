using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Curds.Application.DateTimes;
using Gouda.Application.Persistence;
using Gouda.Application.Security;
using Gouda.Infrastructure.Security;

namespace Gouda.Domain.Security
{
    public class MockSecurityProvider : ISecurity
    {
        private SecurityProvider Security = null;

        public IDateTime Time { get; }
        public IPersistence Persistence { get; }

        public MockSecurityProvider(IDateTime time, IPersistence persistence)
        {
            Time = time;
            Persistence = persistence;

            Security = new SecurityProvider(Time, Persistence);
        }

        public string EncryptPassword(string salt, string password) => Security.EncryptPassword(salt, password);

        public string GenerateSalt() => Curds.Testing.TestSalt;

        public string GenerateSessionIdentifier() => throw new NotImplementedException();
        public Task<Session> GenerateSession(string deviceIdentifier, string email, string password) => Security.GenerateSession(deviceIdentifier, email, password);
    }
}
