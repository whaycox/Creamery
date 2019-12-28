using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Application.ViewModels.Satellite.Domain
{
    public class CheckViewModel : BaseSatelliteViewModel
    {
        public override string ViewName => nameof(CheckViewModel);

        public string Name { get; set; }
        public Guid CheckID { get; set; }
    }
}
