using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Application.ViewModels.Satellite.Domain
{
    using Abstraction;

    public abstract class BaseSatelliteViewModel : ISatelliteViewModel
    {
        public string ViewConcept => nameof(Satellite);
        public abstract string ViewName { get; }
    }
}
