using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Application.Persistence;
using Gouda.Application.Security;

namespace Gouda.Domain.Security
{
    public class MockSecurityProvider : ISecurity
    {
        public IPersistence Persistence { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string EncryptPassword(string salt, string password)
        {
            throw new NotImplementedException();
        }

        public string GenerateSalt()
        {
            throw new NotImplementedException();
        }
    }
}
