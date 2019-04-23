using Curds.Application.DateTimes;
using Curds.Application.Persistence;
using Curds.Application.Security;
using Curds.Application.Security.Command;
using Curds.Domain.Security;
using System;
using System.Threading.Tasks;

namespace Gouda.Domain.Security
{
    public class MockSecurityProvider : ISecurity
    {
        public IDateTime Time { get; }
        public ISecurityPersistence Persistence { get; }

        public bool Validates { get; set; }

        public MockSecurityProvider(IDateTime time, ISecurityPersistence persistence)
        {
            Time = time;
            Persistence = persistence;
            Validates = true;
        }

        private Authentication MockAuth => new MockAuthentication(MockUser.One.ID);

        public Task<Authentication> Login(Login command) => Task.Factory.StartNew(() => MockAuth);
        public Task Logout(LogoutUser command) => Task.Delay(5);
        public Task Logout(LogoutSeries command) => Task.Delay(5);
        public Task<Authentication> ReAuthenticate(ReAuthenticate command) => Task.Factory.StartNew(() => MockAuth);
        public Task<bool> Validate(ValidateSession command) => Task.Factory.StartNew(() => Validates);
        public Task<Authentication> CreateInitialUser(CreateInitialUser command) => Task.Factory.StartNew(() => MockAuth);
    }
}
