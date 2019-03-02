using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Application.Persistence;
using Gouda.Application.Security;
using Gouda.Infrastructure.Security;

namespace Gouda.Domain.Security
{
    public class MockSecurityProvider : ISecurity
    {
        private SecurityProvider Security = new SecurityProvider();

        public IPersistence Persistence { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string EncryptPassword(string salt, string password) => Security.EncryptPassword(salt, password);

        public string GenerateSalt() => Curds.Testing.TestSalt;
    }
}
