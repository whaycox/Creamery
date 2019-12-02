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
        public string Name { get; set; }
        public TextFieldViewModel IPField { get; set; } = new TextFieldViewModel
        {
            Label = LabelDeferredKey.SatelliteIP,
        };
        public CompositeFieldViewModel<SatelliteStatusViewModel> StatusField { get; set; } = new CompositeFieldViewModel<SatelliteStatusViewModel>
        {
            Label = LabelDeferredKey.SatelliteStatus
        };
        public LabelDeferredKey CheckLabel { get; set; } = LabelDeferredKey.Checks;
        public CheckControlsViewModel CheckControls { get; set; } = new CheckControlsViewModel();
    }
}
