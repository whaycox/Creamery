using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gouda.WebApp.Adapters.Abstraction
{
    using Application.ViewModels.Destinations.Abstraction;

    public interface IDestinationAdapter
    {
        string Adapt(IDestinationViewModel destination);
    }
}
