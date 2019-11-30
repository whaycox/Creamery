using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Application.ViewModels.Satellite.Domain
{
    using Abstraction;
    using Enumerations;
    using Input.Domain;
    using DeferredValues.Domain;
    using Common.Domain;

    public class SatelliteViewModel : BaseSatelliteViewModel
    {
        public override string ViewName => nameof(SatelliteViewModel);

        public int ID { get; set; }
        public TextFieldViewModel NameField { get; set; }
        public TextFieldViewModel IPField { get; set; }
        public CompositeFieldViewModel<SatelliteStatusViewModel> StatusField { get; set; }

        public SatelliteViewModel()
        {
            NameField = new TextFieldViewModel { Label = LabelDeferredKey.SatelliteName, };
            IPField = new TextFieldViewModel { Label = LabelDeferredKey.SatelliteIP, };
            StatusField = new CompositeFieldViewModel<SatelliteStatusViewModel> { Label = LabelDeferredKey.SatelliteStatus };
        }
    }
}
