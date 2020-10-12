using System;
using System.Collections.Generic;
using System.Text;

namespace Parmesan.Security.Abstraction
{
    public interface IPasswordHasher
    {
        string Compute(string password, string salt);
    }
}
