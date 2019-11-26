using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Application.ViewModels.Satellite.Abstraction
{
    using Application.Abstraction;
    using Domain;
    using Gouda.Domain;

    public interface ISatelliteSummaryMapper : IViewModelMapper<Satellite, SatelliteSummaryViewModel>
    {
    }
}
