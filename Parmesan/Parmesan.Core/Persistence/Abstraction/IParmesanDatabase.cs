using System;
using System.Collections.Generic;
using System.Text;
using Curds.Persistence.Abstraction;

namespace Parmesan.Persistence.Abstraction
{
    using Parmesan.Domain;

    public interface IParmesanDatabase
    {
        IClientRepository Clients { get; }
    }
}
