using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application.Security;

namespace Gouda.Application.Security
{
    using Persistence;

    public interface ISecurity : IAuthenticator
    {
        IPersistence Persistence { get; }
    }
}
