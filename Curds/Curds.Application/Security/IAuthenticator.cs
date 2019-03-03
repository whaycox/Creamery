using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Application.Security
{
    public interface IAuthenticator
    {
        string GenerateSalt();
        string EncryptPassword(string salt, string password);
    }
}
