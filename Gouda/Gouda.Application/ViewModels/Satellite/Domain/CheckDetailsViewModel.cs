using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Application.ViewModels.Satellite.Domain
{
    using Input.Abstraction;

    public class CheckDetailsViewModel : BaseSatelliteViewModel
    {
        public override string ViewName => nameof(CheckDetailsViewModel);

        public List<IFormInput> Options { get; set; } = new List<IFormInput>();
    }
}
