using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Application.ViewModels.Destinations.Domain
{
    using Abstraction;

    public class DestinationViewModel : IDestinationViewModel
    {
        public string ViewName => nameof(DestinationViewModel);

        public string Name { get; set; }
    }
}
