using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Application.ViewModels.Destinations.Abstraction
{
    using Application.Abstraction;

    public interface IDestinationViewModel : IViewModel
    {
        string Name { get; }
    }
}
