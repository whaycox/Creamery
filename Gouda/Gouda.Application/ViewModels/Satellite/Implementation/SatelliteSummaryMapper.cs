using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Application.ViewModels.Satellite.Implementation
{
    using Abstraction;
    using Domain;
    using Gouda.Domain;

    public class SatelliteSummaryMapper : ISatelliteSummaryMapper
    {
        public SatelliteSummaryViewModel Map(Satellite entity)
        {
            SatelliteSummaryViewModel viewModel = new SatelliteSummaryViewModel { ID = entity.ID };
            viewModel.Name = entity.Name;
            viewModel.IPAddress = entity.IPAddress.ToString();
            viewModel.StatusViewModel.Status = entity.Status;

            return viewModel;
        }
    }
}
