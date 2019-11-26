using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gouda.WebApp.Adapters.Abstraction
{
    using Domain;

    public interface IDestinationCollection
    {
        DestinationItem this[string destination] { get; }
    }
}
